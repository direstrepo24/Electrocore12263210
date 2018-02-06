using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Electro.model.DataContext;
using Electro.model.datatakemodel;
//using Electrocore.Models.datatakemodel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Electrocore.DataContext
{
    public static class DbInitializer
    {
         public static async Task Initialize(MyAppContext _myplanrepository)
        {
          
          // Assembly assembly = Assembly.GetExecutingAssembly();
            var assembly = Assembly.GetExecutingAssembly();
             string resourceName = "Electrocore.DataContext.colombia.json";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            string json = reader.ReadToEnd();
        List<DepartCiudades> items = JsonConvert.DeserializeObject<List<DepartCiudades>>(json);
                            
                                Console.WriteLine("registros count: " + items.Count);
                            //Console.ReadLine();
                            
                            foreach (var item in items)
                            {
                                
                                long idpto=0; 
                               
                                 var departamentos= await _myplanrepository.Departamento.Where(x => x.codigodpto == item.Id).FirstOrDefaultAsync();
                              if(departamentos!=null){
                                        departamentos.Nombre=item.Departamento;
                                        departamentos.codigodpto=item.Id;
                                        
                                        _myplanrepository.Departamento.Update(departamentos);
                                        await _myplanrepository.SaveChangesAsync();//SaveChanges();//SaveChangesAsync();//Commit();
                                        idpto=departamentos.Id;

                              }
                              else{
                                  Departamento departamento = new Departamento();
                                    departamento.Nombre=item.Departamento;
                                    departamento.codigodpto=item.Id;

                                   await  _myplanrepository.Departamento.AddAsync(departamento);
                                _myplanrepository.SaveChanges();
                                idpto=departamento.Id;


                              }
                           //  var ciudades=  await _myplanrepository.Ciudad.Where(x => x.departmento.codigodpto == item.Id).ToListAsync();//_myplanrepository.FindAsync(w=>w.codigoplan==item.codigoplan);
                             var ciudades=item.Ciudades.ToList();
                               
                                if(ciudades.Count>0){
                                        foreach (var ciudad in ciudades)
                                        {
                                            var ciudadexit=await _myplanrepository.Ciudad.Where(x=>x.Nombre==ciudad && x.departmento.codigodpto==item.Id).FirstOrDefaultAsync();
                                             if(ciudadexit!=null)
                                             {
                                                 ciudadexit.Nombre=ciudad;
                                                _myplanrepository.Ciudad.Update(ciudadexit);
                                                await _myplanrepository.SaveChangesAsync();//SaveChanges();//SaveChangesAsync();//Commit();
  


                                             }
                                             else{

                                                 Ciudad ciudadnew=new Ciudad();
                                                 ciudadnew.Nombre=ciudad;
                                                 ciudadnew.departmentoId=idpto;

                                                       await  _myplanrepository.Ciudad.AddAsync(ciudadnew);
                                                     _myplanrepository.SaveChanges();
                                                  Console.WriteLine("Entidad almacenada");

                                             }
                                             

                                            
                                        }

                                       
                                         
                                }
                               
                              
                               
                               //  Console.ReadLine();
                            }
                             Console.WriteLine("procesos terminado");
                          
                        }
                        
                    }
                }

        
    }
}