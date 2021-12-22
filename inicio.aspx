<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inicio.aspx.cs" Inherits="inicio" %>

<!DOCTYPE html>

<html lang="es">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <title>ExpressSAS</title>
    <link href="App_Themes/estilos/normalize.css" rel="stylesheet" />
    <link href="App_Themes/Estilos/fonts_roboto.css" rel="stylesheet" />
    <link href="App_Themes/estilos/fonts.css" rel="stylesheet" />
    <link href="App_Themes/estilos/inicio.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images/iconos/icoExpress2.png" />
    <link rel="apple-touch-icon" href="~/images/iconos/icoaneta2.png">
    <meta name="theme-color" content="#FF6600">
</head>
<body>
    <form id="form1" runat="server">
        <!--cabecera-->
        <header class="main-header ">

            <div class="l-container main-header__block">
                <img src="App_Themes/img/iconos/icoaneta.png" alt="logo ANETA" class="main-logo__header">
                <img src="App_Themes/img/iconos/anetapalabra.png" alt="ANETA" class="main-logo2__header">

                <div class="main-menu-toggle" id="main-menu-toggle"></div>
                <nav class="main-nav" id="main-nav">
                    <ul class="main-menu">
                        <li class="main-menu__item">
                            <asp:TextBox runat="server" ID="txtUsuario" class="main-menu__text" placeholder="Usuario"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtContraseña" class="main-menu__text" placeholder="Contraseña" Font-Bold="False" TextMode="Password"></asp:TextBox>

                            <asp:Button runat="server" ID="btnIniciarSesion" Text="Iniciar sesión " Class="main-menu__link main-menu__boton" OnClick="btnIniciarSesion_Click" />

                            <asp:Label runat="server" class="main-menu__link" ID="lblMensaje"></asp:Label>

                        </li>
                    </ul>

                </nav>
            </div>


        </header>
        <!--banner principal -->
        <div class="main-banner l-section">
            <img src="App_Themes/img/publicidad/xpress.png"
                alt="Banner ANETA"                >
            <div class="main-banner__content">
                <img class="main-banner__logo" src="App_Themes/img/iconos/icoaneta3.png" alt="Ícono ANETA">
            </div>

        </div>

        <!--principal-->
        <div class="principal">
            <section class="l-section l-container subtituloInicio">
                <p class="l-50 center-block center-content p-subtitulo">
                    Gestión de transporte de productos y servicios.
                </p>
            </section>
        </div>

        <!--pie de página-->
        <footer class="main-footer">
            <div class="l-container main-footer__block">
                <img src="App_Themes/img/iconos/logo_correo.jpg" alt="logo ANETA" class="main-logo__footer">
                <p>Derechos reservados © 2021 ANETA</p>
            </div>
        </footer>
        <script src="App_Themes/js/modernizr-custom.js"></script>
        <script src="App_Themes/js/jquery-3.2.1.js"></script>
        <script src="App_Themes/js/inicio.js"></script>
    </form>

</body>
</html>
