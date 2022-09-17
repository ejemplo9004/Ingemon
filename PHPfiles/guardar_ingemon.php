<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
    } else {
        if (isset($_POST["name"]) &&
            isset($_POST["phenotype"]) &&
            isset($_POST["maxHealth"]) &&
            isset($_POST["id_jugador"])){


            $nombre_ingemon = $_POST["name"];
            $fenotipo = $_POST["phenotype"];
            $salud = $_POST["maxHealth"];
            $id_jugador = $_POST["id_jugador"];

            $sql = "SELECT * FROM `ingemones` WHERE phenotype = '".$fenotipo."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                echo '{"codigo":405,"mensaje": "Ya hay un ingemon con este fenotipo","respuesta":""}';
            }else {
                $sql = "INSERT INTO `ingemones` (`id`, `name`, `phenotype`, `maxHealth`, `id_jugador`) 
                VALUES (NULL, '".$nombre_ingemon."', '".$fenotipo."', '".$salud."', '".$id_jugador."');";

                if ($conn->query($sql) === TRUE){
                    $sql = "SELECT * FROM `ingemones` WHERE phenotype = '".$fenotipo."';";
                    $resultado = $conn->query($sql);
                    $texto = "";
                    while ($row = $resultado->fetch_assoc()){
                        $text = "{#id#:".$row['id'].
                            ",#ingemon#:#".$row['name'].
                            "#,#fenotipo#:#".$row['phenotype'].
                            "#,#salud#:".$row['maxHealth'].
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