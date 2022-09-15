<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
    } else {


        if (isset($_POST["nombre_usuario"]) &&
            isset($_POST["pass"]) &&
            isset($_POST["pass2"]) &&
            isset($_POST["jugador"]) &&
            isset($_POST["nivel"])){


            $nombre_usuario = $_POST["nombre_usuario"];
            $pass = $_POST["pass"];
            $pass2 = $_POST["pass2"];
            $jugador = $_POST["jugador"];
            $nivel = $_POST["nivel"];

            $sql = "SELECT * FROM `usuarios` WHERE nombre_usuario = '".$nombre_usuario."' and pass ='".$pass."';";
            $resultado = $conn->query($sql);
            if ($resultado->num_rows > 0){
                $sql = "UPDATE `usuarios` SET  `pass` = '".$pass2."', `jugador` = '".$jugador."', `nivel` = '".$nivel."' WHERE nombre_usuario = '".$nombre_usuario."';";
                $conn->query($sql);
                $sql = "SELECT * FROM `usuarios` WHERE nombre_usuario = '".$nombre_usuario."';";
                $resultado = $conn->query($sql);
                $texto = '';
            while($row = $resultado->fetch_assoc()){
                $texto =
                    "{#id#:".$row["id_usuario"].
                    ",#usuario#:#".$row["nombre_usuario"].
                    "#,#pass#:#".$row["pass"].
                    "#,#jugador#:".$row["jugador"].
                    ",#nivel#:".$row["nivel"].
                    "}";
            }
                //lo encuentra
                echo '{"codigo":206,"mensaje": "Usuario editado con exito","respuesta":"'.$texto.'"}';
            }else {
                echo '{"codigo":204,"mensaje": "el usuario o contraseña incorrectos","respuesta":"0"}';
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