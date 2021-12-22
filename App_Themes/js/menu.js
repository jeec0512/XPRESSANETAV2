$(document).ready(function(){

	// MOSTRANDO Y OCULTANDO MENU
	$('#button-menu').click(function(){
		if($('#button-menu').attr('class') == 'icon-hamb' ){

			$('.navegacion').css({'width':'100%', 'background':'rgba(0,0,0,.5)'}); // Mostramos al fondo transparente
			$('#button-menu').removeClass('icon-hamb').addClass('icon-salir'); // Agregamos el icono X
			$('.navegacion .menu').css({'left':'0px'}); // Mostramos el menu

		} else{

			$('.navegacion').css({'width':'0%', 'background':'rgba(0,0,0,.0)'}); // Ocultamos el fonto transparente
			$('#button-menu').removeClass('icon-salir').addClass('icon-hamb'); // Agregamos el icono del Menu
			$('.navegacion .submenu').css({'left':'-500px'}); // Ocultamos los submenus
			$('.navegacion .menu').css({ 'left': '-500px' }); // Ocultamos el Menu

		}
	});

	// MOSTRANDO SUBMENU
	$('.navegacion .menu > .item-submenu a').click(function(){
		
		var positionMenu = $(this).parent().attr('menu'); // Buscamos el valor del atributo menu y lo guardamos en una variable
		console.log(positionMenu); 

		$('.item-submenu[menu=' + positionMenu + '] .submenu').css({ 'left': '0px' }); // Mostramos El submenu correspondiente

	});

	// OCULTANDO SUBMENU
	$('.navegacion .submenu li.icon-left').click(function () {

	    $(this).parent().css({ 'left': '-320px' }); // Ocultamos el submenu

	});

});