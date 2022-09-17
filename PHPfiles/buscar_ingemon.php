<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar a bd","respuesta":""}';
    } else {
        if (isset($_POST["id_jugador"])){

            $id_jugador = $_POST["id_jugador"];

            $sql = "SELECT * FROM `ingemones` WHERE id_jugador = '".$id_jugador."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                $textos = [];
                $texto = "";
                while ($row = $resultado->fetch_assoc()){
                    $texto = "{#id#:".$row['id'].
                        ",#name#:#".$row['name'].
                        "#,#phenotype#:#".$row['phenotype'].
                        "#,#maxHealth#:#".$row['maxHealth'].
                        "#,#id_jugador#:".$row['id_jugador'].
                        "}";
                    array_push($textos, $texto);
                }
                $string_version = implode('!', $textos);
                echo '{"codigo":210,"mensaje": "Ingemones encontrados correctamente","respuesta":"'.$string_version.'"}';
            }else {
                echo '{"codigo":410,"mensaje": "El jugador no tiene ingemones,"respuesta":""}';
            }

        }else{
            echo '{"codigo":402,"mensaje": "faltan datos para ejecutar la accion solicitada","respuesta":""}';
        }
    }
} catch (Exception $e) {
    echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
}
include "footer.php";
