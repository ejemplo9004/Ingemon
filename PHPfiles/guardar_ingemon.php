<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
    } else {
        if (isset($_POST["nombre_ingemon"]) &&
            isset($_POST["fenotipo"]) &&
            isset($_POST["salud"])){


            $nombre_ingemon = $_POST["nombre_ingemon"];
            $fenotipo = $_POST["fenotipo"];
            $salud = $_POST["salud"];
            $id_jugador = $_POST["id_jugador"];

            $sql = "SELECT * FROM `ingemones` WHERE fenotipo = '".$fenotipo."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                echo '{"codigo":405,"mensaje": "Ya hay un ingemon con este nombre y/o fenotipo","respuesta":""}';
            }else {
                $sql = "INSERT INTO `ingemones` (`id_ingemon`, `nombre_ingemon`, `fenotipo`, `salud`, `id_jugador`) 
                VALUES (NULL, '".$nombre_ingemon."', '".$fenotipo."', '".$salud."', '".$id_jugador."');";

                if ($conn->query($sql) === TRUE){
                    $sql = "SELECT * FROM `ingemones` WHERE fenotipo = '".$fenotipo."';";
                    $resultado = $conn->query($sql);
                    $texto = "";
                    while ($row = $resultado->fetch_assoc()){
                        $text = "{#id#:".$row['id_ingemon'].
                            ",#ingemon#:#".$row['nombre_ingemon'].
                            "#,#fenotipo#:#".$row['fenotipo'].
                            "#,#salud#:".$row['salud'].
                            ",#id_jugador#:".$row['id_jugador'].
                            "}";
                    }
                    echo '{"codigo":207,"mensaje": "ingemon creado correctamente","respuesta":"'.$texto.'"}';
                }else {
                    echo '{"codigo":406,"mensaje": "error intentando crear el ingemon","respuesta":""}';
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