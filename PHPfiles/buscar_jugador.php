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

            $sql = "SELECT * FROM `jugadores` WHERE id_jugador = '".$id_jugador."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                $texto = "";
                while ($row = $resultado->fetch_assoc()){
                    $texto = "{#id_jugador#:".$row['id_jugador'].
                        ",#oro#:#".$row['oro'].
                        "#,#xp#:#".$row['xp'].
                        "#,#grupo#:".$row['grupo'].
                        "}";
                }
                echo '{"codigo":209,"mensaje": "jugador encontrado correctamente","respuesta":"'.$texto.'"}';
            }else {
                echo '{"codigo":409,"mensaje": "No existe jugador con esta id","respuesta":""}';
                }

        }else{
            echo '{"codigo":402,"mensaje": "faltan datos para ejecutr la accion solicitada","respuesta":""}';
        }
        }
} catch (Exception $e) {
    echo '{"codigo":400,"mensaje": "error intentando conectar","respuesta":""}';
}
include "footer.php";