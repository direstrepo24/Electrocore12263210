using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime.Internal.Util;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using electrocore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace electrocore.Controllers
{
    [Route("api/[controller]")]
    public class FotoController:Controller
    {

 private  IAmazonS3 S3Client { get; set; }
        IHostingEnvironment  _hostingEnvironment;
       // private AmazonS3Client _s3Client = new AmazonS3Client(RegionEndpoint.EUWest2);
       //private AmazonS3Client _s3Client;
        private static string _bucketName = "datatakefiles";//this is my Amazon Bucket name




   
        
        public FotoController(IHostingEnvironment hostingEnvironment,IAmazonS3 s3Client){
            _hostingEnvironment=hostingEnvironment;
             
             this.S3Client = s3Client;
           
            
        }

        /* 
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", 
                        Path.GetFileName(file.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            var response= new ResponseViewModel();
            response.Message="Ok";

            return Ok(response);

           // return RedirectToAction("Files");
        }
*/


/* 
        [HttpPost]
        public IActionResult UploadFilesAjax()
        {
            long size = 0;
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                filename = hostingEnv.WebRootPath + $@"\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                file.CopyTo(fs);
                fs.Flush();
                }
            }
            string message = $"{files.Count} file(s)/{size} bytes uploaded successfully!";
            return Json(message);
        }
*/


        [HttpPost]
        [Route("PostFotoS3")] 
        public async Task<ActionResult> PostFotoS3([FromBody]RequestFotoViewModel foto)
        {
            var response= new ResponseViewModel();
            
            if (!ModelState.IsValid)
            {
                response.IsSuccess=false;
                       response.Message="Modelo no valido";
                        return Ok(response);
            }
     
            var responseUpload= await UploadFileAwsS3(foto.ImageArray);

         var rutaFoto= "/Fotos/cedula.jpg";
           string replaceFoto =rutaFoto.Replace("/Fotos/", "Fotos/");
            var responseDeleting= await DeletingAnObject(_bucketName,replaceFoto);


            return Ok(response);



            /* 
               var requestPlain = new PutObjectRequest
                {
                    BucketName = "datatakefiles",
                    Key = "cedula.jpg",
                  //FilePath = "wwwroot/Fotos/a9a86dc4-8742-46aa-8e28-b42d79153059.jpg",
                    ContentType = "image/jpg",
                    //CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                    InputStream = fotoArrayNew      //SEND THE FILE STREAM,   
                };

                var cancellationToken = new CancellationToken();
                var responseUpload = await S3Client.PutObjectAsync(requestPlain, cancellationToken);*/
           
          
        }


        private async Task<ResponseViewModel> UploadFileAwsS3(byte[] imageArray){
            var response= new ResponseViewModel();
            try
            {

            MemoryStream fotoArray = new MemoryStream(imageArray);
            Image originalImage = Image.FromStream(fotoArray);
            

            //Orientation Image
            if (Array.IndexOf(originalImage.PropertyIdList, 274) > -1)
            {
                        var orientation = (int)originalImage.GetPropertyItem(274).Value[0];
                        switch (orientation)
                        {
                            case 1:
                                // No rotation required.
                                break;
                            case 2:
                                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                break;
                            case 3:
                                originalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 4:
                                originalImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                                break;
                            case 5:
                                originalImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                                break;
                            case 6:
                                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 7:
                                originalImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                                break;
                            case 8:
                                originalImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        // This EXIF data is now invalid and should be removed.
                        originalImage.RemovePropertyItem(274);
            }
                    
            var imageByte= ImageToByteArray(originalImage);
            Stream fotoArrayNewDelete = new MemoryStream(imageByte);

            Stream fotoArrayNew = new MemoryStream(imageByte);
            
            var requestPlain = new PutObjectRequest
            {
                    BucketName = "datatakefiles",
                    Key = "Fotos/cedula.jpg",
                  //FilePath = "wwwroot/Fotos/a9a86dc4-8742-46aa-8e28-b42d79153059.jpg",
                    ContentType = "image/jpg",
                    //CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                    InputStream = fotoArrayNewDelete      //SEND THE FILE STREAM,   
            };
            var cancellationToken = new CancellationToken();
            var responseUpload = await S3Client.PutObjectAsync(requestPlain, cancellationToken);

          
            var guid1 = Guid.NewGuid().ToString();
            var guid2 = Guid.NewGuid().ToString();
            var file = string.Format("{0}{1}.jpg", guid1,guid2);
            var folder = "Fotos";
            var rutaFoto= string.Format("{0}/{1}",folder,file);
            var rutaFotoBD= string.Format("/{0}",rutaFoto);
            var request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                ContentType = "image/jpg",
                //CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                Key = rutaFoto,
                InputStream = fotoArrayNew,//SEND THE FILE STREAM,                            
            };
          
                        
            //await S3Client.PutObjectAsync(request);
            var cancellationTokenFoto = new CancellationToken();
            var responseUploadFoto = await S3Client.PutObjectAsync(request, cancellationTokenFoto);

            response.IsSuccess=true;
            response.Message="file uploaded";
            response.Result=responseUploadFoto;
            return response;


            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                    response.IsSuccess=false;
                    response.Message=string.Format("Please check the provided AWS Credentials.");
                    return response;
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                     response.IsSuccess=false;
                     response.Message=string.Format("An error occurred with the message {0} when writing an object",amazonS3Exception.Message);
                      return response;
                }
            }
        }

        private async  Task<ResponseViewModel> DeletingAnObject(string bucketName,string keyName)
        {
            var response= new ResponseViewModel();
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };

              await S3Client.DeleteObjectAsync(request);

                response.IsSuccess=true;
                response.Message="file delete";


                return response;


              
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                    response.IsSuccess=false;
                    response.Message=string.Format("Please check the provided AWS Credentials.");
                    return response;
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when deleting an object", amazonS3Exception.Message);
                     response.IsSuccess=false;
                     response.Message=string.Format("An error occurred with the message {0} when deleting an object",amazonS3Exception.Message);
                      return response;
                }
            }
        }

        private  byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
        using (var ms = new MemoryStream())
        {
            imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
            return  ms.ToArray();
        }
        }


        public static Stream ResizeImageFile(Stream imageFileStream, int targetSize) // Set targetSize to 1024
        {
            byte[] imageFile = StreamToByteArray(imageFileStream);
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return new MemoryStream(m.GetBuffer());
                    }
                }
            }
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }


 

        [HttpPost]
        public async Task<ActionResult> PostFoto([FromBody]RequestFotoViewModel foto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           /// byte[] fotoByte=Encoding.ASCII.GetBytes(foto.ImageArray);
            /* 
            if (foto.ImageArray != null && foto.ImageArray.Length > 0)
            {
                var streamRemote = new MemoryStream(foto.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "wwwroot/Fotos";  
                var formFile = new FormFile(streamRemote , 0, streamRemote.Length, "name", file);
                var fileName= ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim();
                fileName= this.EnsureFilename(fileName);
                using(FileStream fileStream = System.IO.File.Create(this.Getpath(fileName))){
                }
            }*/
            var response= new ResponseViewModel();
            
            /*if (foto.ImageArray != null && foto.ImageArray.Length > 0)
            {
                var streamRemote = new MemoryStream(fotoByte);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "/Fotos";
                var rutaFoto= string.Format("{0}/{1}",folder,file);
                string pathExist= _hostingEnvironment.WebRootPath+folder;// _hostingEnvironment.WebRootPath es Igual a "wwwroot"
                if(!Directory.Exists(pathExist)){
                   Directory.CreateDirectory(pathExist);
                   response.Message="No existe el directorio, se creo";
                }else{
                   response.Message="Si existe el directorio"; 
                }
                var formFile = new FormFile(streamRemote , 0, streamRemote.Length, "name", file);
                if (formFile == null || formFile.Length == 0)
                return Content("file not selected");
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), pathExist, 
                        Path.GetFileName(formFile.FileName));
                //Ruta foto
                //var fullPath = string.Format("{0}", path);
               // foto.Ruta= rutaFoto;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }*/

            //byte[] bArray = Encoding.UTF8.GetBytes (foto.ImageArray);

            foto.Ruta=await postUploadImage(foto.ImageArray);
            //string fileName= ContentDispositionHeaderValue.Parse(foto);
           
            response.IsSuccess=true;
            response.Result=foto;
            return Ok(response);
        }


        private async Task<string> postUploadImage(byte[] imageArray)
        {
            string rutaFoto=string.Empty;
            if (imageArray != null && imageArray.Length > 0)
            {
                var streamRemote = new MemoryStream(imageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "/Fotos";
                rutaFoto= string.Format("{0}/{1}",folder,file);
                string pathExist= _hostingEnvironment.WebRootPath+folder;// _hostingEnvironment.WebRootPath es Igual a "wwwroot"
                if(!Directory.Exists(pathExist)){
                   Directory.CreateDirectory(pathExist);
                  //string message="No existe el directorio, se creo";
                }else{
                  //string message="Si existe el directorio"; 
                }
                var formFile = new FormFile(streamRemote , 0, streamRemote.Length, "name", file);
             
                if (formFile == null || formFile.Length == 0)
                    return "file not selected";

                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), pathExist, 
                        Path.GetFileName(formFile.FileName));
                //Ruta foto
                //var fullPath = string.Format("{0}", path);
                //foto.Ruta= rutaFoto;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

             
            }

            return rutaFoto;
        }

        private string EnsureFilename(string fileName)
        {
            if(fileName.Contains("\\"))
                fileName= fileName.Substring(fileName.LastIndexOf("\\")+1);
            return fileName;
        }

        private string Getpath(string fileName)
        {
            string path= _hostingEnvironment.WebRootPath+"\\upload\\";
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path+fileName;
            
        }
    }
}