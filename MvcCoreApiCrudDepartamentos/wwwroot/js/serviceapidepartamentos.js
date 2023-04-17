var urlApi = "https://apicorecruddepartamentos2023.azurewebsites.net/";

//METODO CALLBACK, ES DECIR, QUE RECIBIRA ALGO (O NO)
//Y DEVOLVERA UNA RESPUESTA ASYNCRONA
function getJsonDepartamentosAsync(callBack) {
    //ESTO DEBE DEVOLVER ALGO
    var request = "/api/departamentos";
    $.ajax({
        url: urlApi + request,
        method: "GET",
        success: function (data) {
            callBack(data);
        }
    })
}

function eliminarDepartamentoAsync(id, callBack) {
    var request = "/api/departamentos/" + id;
    $.ajax({
        url: urlApi + request, 
        type: "DELETE",
        success: function () {
            callBack();
        }
    })
}

function convertDepartamentoJson(num, nom, loc) {
    var dept = new Object();
    dept.idDepartamento = num;
    dept.nombre = nom;
    dept.localidad = loc;
    var objJson = JSON.stringify(dept);
    return objJson;
}

function insertarDepartamentoAsync(num, nom, loc, callBack) {
    var objJson = convertDepartamentoJson(num, nom, loc);
    var request = "/api/departamentos";
    $.ajax({
        url: urlApi + request,
        type: "POST",
        data: objJson,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    })
}

function updateDepartamentoAsync(num, nom, loc, callBack) {
    var objJson = convertDepartamentoJson(num, nom, loc);
    var request = "/api/departamentos";
    $.ajax({
        url: urlApi + request,
        type: "PUT",
        data: objJson,
        contentType: "application/json",
        success: function () {
            callBack();
        }
    })
}

