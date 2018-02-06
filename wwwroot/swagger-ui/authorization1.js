(function () {
    $(function () {
        var tokenUi = '<div class="input">' +
            '<h2>Authenticate</h2>' +
            '<input placeholder="Username" id="input_username" name="username" type="text" size="25">' +
            '<input placeholder="Password" id="input_password" name="password" type="password" size="25">' +
            '<input placeholder="grant_type" id="input_grant_type" name="grant_type" type="text" size="25">' +
            
            '<input id="input_authenticate" name="authenticate" type="button" value="Get token">' +
            '</div>';
        $(tokenUi).insertBefore("#resources_container");

        $("#input_authenticate").click(function () {
            var username = $("#input_username").val();
            var password = $("#input_password").val();
            var grant_type = $("#input_grant_type").val();
            getToken(username, password, grant_type);
        });
    });
    function getToken(username, password, grant_type) {
        var request = {username: username, password: password, grant_type:grant_type };

        $.ajax({
           // contentType: "application/json;charset=UTF-8",
            type: "post",
            url: "/connect/token",
            dataType: "json",
            data: request,
            success: function (data) {
                addAuthorization(data.token_type + " " + data.access_token);
            }
        });
    };
    function addAuthorization(key) {
        window.swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", key, "header"));
    };
})();