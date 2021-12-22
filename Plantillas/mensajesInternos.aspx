﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mensajesInternos.aspx.cs" Inherits="Plantillas_mensajesInternos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Entrega de títulos</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            background-repeat: no-repeat;
        }

        .main {
            width: 90vw;
            margin-top: 2rem;
            margin-right: auto;
            margin-left: auto;
        }

        h1 {
            color: #143356;
            font-weight: bold;
            font-size: 1rem;
        }

        .main-header {
            margin-right: auto;
            margin-left: auto;
            border: 0.1rem solid #e35000;
            width: 60%;
            padding: .2rem;
            display: flex;
            position: relative;
            top: 50px;
        }

        .header__item {
            margin-left: auto;
            margin-right: auto;
            margin-bottom: auto;
            margin-top: auto;
        }

        .main-body {
            position: relative;
            margin-right: auto;
            margin-left: auto;
            width: 60%;
            border: 0.1rem solid #e35000;
            padding: 2rem;
          
            top: 50px;
            height: 80vh;
        }

       

        .body__footer {
            padding-top: 2rem;
            display: grid;
            grid-template-column: 1fr;
        }

        .footer-item {
            font-size: .8rem;
            padding-left: .5rem;
        }

        .link {
					font-size: 1rem;
            		 color: orange;
            		 text-decoration: none;
            		 margin:10px;

        		}

        .link:hover {
					font-size: 1rem;
            		 color: orange;
            		 text-decoration: none;
            		 margin:10px;

        		}

        .main-body .parrafo {
            		position: absolute;
        			top: 50%;
					left: 50%;
					transform: translate(-50%, -50%);

        		}

        .imagen{
        	opacity: 1;
        	width: 100%;
 			height: 100%
        		
        }

        
        .body__item{
			color: white;
			margin-bottom: .5rem;

		}
        .main-body .parrafo {
          			
            position: absolute;
        	width: 70%;	
            height:100%	;
        	top: 2.3rem;
			left: 2.5rem;
			transform: translate(-50%, -50%);

        	}

		/*@media screen and (min-width: 480px) {
			.main-body .parrafo {
          			
         position: absolute;
        			
        	top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);

        	}
		}

		@media screen and (max-width: 480px) {
			.main-body .parrafo {
          			
        position: absolute;
        			
        	top: 50%;
			left: 50%;
			transform: translate(-50%, -50%);

        	}
		}*/
    </style>
</head>
<body>
    <form class="main">
        <div class="main-header">
            <img src="http://181.188.214.60:5090/acefdos/images/iconos/icoExpress2.png" alt="ANETA"/>
            <div class="header__item">
                <h1>ESCUELA DE CONDUCCIÓN ANETA</h1>
            </div>
        </div>

        <div class="main-body">
            <img src="http://181.188.214.60:5090/acefdos/images/fondos/fondo5.png" class="imagen" />
            <div class="parrafo">
                <p class="body__item">Estimado/a <span>COLABORADOR</span></p>


                <p class="body__item">MENSAJE</p>

               


                <div class="body__footer">

                    <p class="body__item">Saludos cordiales,</p>

                    <p class="body__item">ADMESCUELA / ANETA</p>
                    <p class="body__item">TITADMINISTRADOR</p>
                    <p class="body__item">Escuelas de Conducción</p>
                    <p class="body__item">DIRECCIONESCUELA</p>
                    <p class="body__item">CIUDADESCUELA - Ecuador</p>
                    <p class="body__item">Fijo TELEFONOESCUELA</p>
                    <p class="body__item">PAGINAWEBESCUELA</p>
                </div>
            </div>
        </div>
        <script>
            jQuery(document).ready(function () {

                /*
                    Fullscreen background
                */
                $.backstretch("../images/fondos/fondo3.jpg");

                /*
                    Form validation
                */


            });

        </script>
    </form>
</body>
</html>
