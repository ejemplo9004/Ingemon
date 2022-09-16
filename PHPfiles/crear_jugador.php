<?php
include "header.php";
try {

//conexion a bd
    $conn = mysqli_connect($db_servidor, $db_usuario, $db_pass, $db_baseDatos);
    if (!$conn) {
        echo '{"codigo":400,"mensaje": "error intentando conectar a bd","respuesta":""}';
    } else {
        if (isset($_POST["id_jugador"]) &&
            isset($_POST["oro"]) &&
            isset($_POST["xp"]) &&
            isset($_POST["grupo"])){

            $id_jugador = $_POST["id_jugador"];
            $oro = $_POST["oro"];
            $xp = $_POST["xp"];
            $grupo = $_POST["grupo"];

            $sql = "SELECT * FROM `jugadores` WHERE id_jugador = '".$id_jugador."';";
            $resultado = $conn->query($sql);

            if ($resultado->num_rows > 0){
                echo '{"codigo":407,"mensaje": "ya existe un jugador creado","respuesta":""}';
            }else {
                $sql = "INSERT INTO `jugadores`(`id_jugador`, `oro`, `xp`, `grupo`) 
                        VALUES ('.$id_jugador.','.$oro.','.$xp.','.$grupo.');";

                if ($conn->query($sql) === TRUE){
                    $sql = "SELECT * FROM `jugadores` WHERE id_jugador = '".$id_jugador."';";
                    $resultado = $conn->query($sql);
                    $texto = "";
                    while ($row = $resultado->fetch_assoc()){
                        $texto = "{#id#:".$row['id_jugador'].
                            ",#oro#:#".$row['oro'].
                            "#,#xp#:#".$row['xp'].
                            "#,#grupo#:".$row['grupo'].
                            "}";
                    }
                    echo '{"codigo":208,"mensaje": "jugador creado correctamente","respuesta":"'.$texto.'"}';
                }else {
                    echo '{"codigo":408,"mensaje": "error intentando crear el jugador","respuesta":""}';
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