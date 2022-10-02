<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
    } else {


        if (isset($_POST["id_jugador"]) &&
            isset($_POST["oro2"]) &&
            isset($_POST["xp2"])){


            $id_jugador = $_POST["id_jugador"];
            $oro2 = $_POST["oro2"];
            $xp2 = $_POST["xp2"];

            $sql = "SELECT * FROM `jugadores` WHERE id_jugador = '".$id_jugador."';";
            $resultado = $conn->query($sql);
            if ($resultado->num_rows > 0){
                $sql = "UPDATE `jugadores` SET  `oro` = '".$oro2."', `xp` = '".$xp2."' WHERE id_jugador = '".$id_jugador."';";
                $conn->query($sql);
                $sql = "SELECT * FROM `jugadores` WHERE id_jugador = '".$id_jugador."';";
                $resultado = $conn->query($sql);
                $texto = '';
                while($row = $resultado->fetch_assoc()){
                    $texto =
                        "{#id#:".$row["id_jugador"].
                        ",#oro#:#".$row["oro"].
                        "#,#xp#:#".$row["xp"].
                        "#,#grupo#:".$row["grupo"].
                        "}";
                }
                //lo encuentra
                echo '{"codigo":209,"mensaje": "Jugador editado con exito","respuesta":"'.$texto.'"}';
            }else {
                echo '{"codigo":210,"mensaje": "el jugador no existe","respuesta":"0"}';
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