<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
    } else {

        if (isset($_GET["nombre_usuario"])){


            $nombre_usuario = $_GET["nombre_usuario"];
            $sql = "SELECT * FROM `usuarios` WHERE nombre_usuario = '".$nombre_usuario."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                echo '{"codigo":202,"mensaje": "usuario existe en el sistema","respuesta":"'.$resultado->num_rows.'"}';
            }else {
                echo '{"codigo":203,"mensaje": "el usuario no existe","respuesta":"0"}';
            }
        }else{
            echo '{"codigo":402,"mensaje": "faltan datos para ejecutar la accion solicitada","respuesta":""}';
        }
    }
} catch (Exception $e) {
    echo ($e);
    echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';

}

include "footer.php";