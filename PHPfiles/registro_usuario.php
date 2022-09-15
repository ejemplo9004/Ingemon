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
            isset($_POST["jugador"]) &&
            isset($_POST["nivel"])){


        $nombre_usuario = $_POST["nombre_usuario"];
        $pass = $_POST["pass"];
        $jugador = $_POST["jugador"];
        $nivel = $_POST["nivel"];

            $sql = "SELECT * FROM `usuarios` WHERE nombre_usuario = '".$nombre_usuario."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                echo '{"codigo":403,"mensaje": "ya existe un usuario con ese nombre","respuesta":""}';
            }else {
                $sql = "INSERT INTO `usuarios` (`id_usuario`, `nombre_usuario`, `pass`, `jugador`, `nivel`) 
                VALUES (NULL, '".$nombre_usuario."', '".$pass."', '".$jugador."', '".$nivel."');";

                if ($conn->query($sql) === TRUE){
                    $sql = "SELECT * FROM `usuarios` WHERE nombre_usuario = '".$nombre_usuario."';";
                    $resultado = $conn->query($sql);
                    $texto = "";
                    while ($row = $resultado->fetch_assoc()){
                        $text = "{#id#:".$row['id_usuario'].
                            ",#usuario#:#".$row['nombre_usuario'].
                            "#,#pass#:#".$row['pass'].
                            "#,#jugador#:".$row['jugador'].
                            ",#nivel#:".$row['nivel'].
                            "}";
                    }
                    echo '{"codigo":201,"mensaje": "usuario creado correctamente","respuesta":"'.$texto.'"}';
                }else {
                    echo '{"codigo":401,"mensaje": "error intentando crear el usuario","respuesta":""}';
                }
            }
        }else{
            echo '{"codigo":402,"mensaje": "faltan datos para ejecutr la accion solicitada","respuesta":""}';
        }
    }
} catch (Exception $e) {
    echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';

}

include "footer.php";