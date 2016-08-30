<!DOCTYPE html>
<!--[if IE 8]>			<html class="ie ie8"> <![endif]-->
<!--[if IE 9]>			<html class="ie ie9"> <![endif]-->
<!--[if gt IE 9]><!-->
<html>
<!--<![endif]-->
<head>
<?php
include 'header.php';
?>

</head>

<!--
AVAILABLE BODY CLASSES:

smoothscroll 			= create a browser smooth scroll
enable-animation		= enable WOW animations

bg-grey					= grey background
grain-grey				= grey grain background
grain-blue				= blue grain background
grain-green				= green grain background
grain-blue				= blue grain background
grain-orange			= orange grain background
grain-yellow			= yellow grain background

boxed 					= boxed layout
pattern1 ... patern11	= pattern background
menu-vertical-hide		= hidden, open on click

BACKGROUND IMAGE [together with .boxed class]
data-background="assets/images/boxed_background/1.jpg"
-->
<body class="smoothscroll enable-animation">

	<!-- wrapper -->
	<div id="wrapper">

		<!--
		AVAILABLE HEADER CLASSES

		Default nav height: 96px
		.header-md 		= 70px nav height
		.header-sm 		= 60px nav height

		.noborder 		= remove bottom border (only with transparent use)
		.transparent	= transparent header
		.translucent	= translucent header
		.sticky			= sticky header
		.static			= static header
		.dark			= dark header
		.bottom			= header on bottom

		shadow-before-1 = shadow 1 header top
		shadow-after-1 	= shadow 1 header bottom
		shadow-before-2 = shadow 2 header top
		shadow-after-2 	= shadow 2 header bottom
		shadow-before-3 = shadow 3 header top
		shadow-after-3 	= shadow 3 header bottom

		.clearfix		= required for mobile menu, do not remove!

		Example Usage:  class="clearfix sticky header-sm transparent noborder"
	-->
		<div id="header" class="sticky transparent header-md clearfix">

			<!-- TOP NAV -->
			<header id="topNav">
				<div class="container">

					<!-- Mobile Menu Button -->
					<button class="btn btn-mobile" data-toggle="collapse"
						data-target=".nav-main-collapse">
						<i class="fa fa-bars"></i>
					</button>

					<!-- Logo -->
					<a class="logo pull-left scrollTo" href="#top"> <img
						src="assets/imagenes/garage_landing_logo_menu.png" alt="" /> <img
						src="assets/imagenes/garage_landing_logo_menu.png" alt="" />
					</a>

					<!--
				Top Nav

				AVAILABLE CLASSES:
				submenu-dark = dark sub menu
			-->
					<div class="navbar-collapse pull-right nav-main-collapse collapse">
						<nav class="nav-main">

							<!--
					.nav-onepage
					Required for onepage navigation links

					Add .external for an external link!
				-->
							<ul id="topMain" class="nav nav-pills nav-main nav-onepage">
								<li class="active">
									<!-- HOME --> <a href="#slider"> HOME </a>
								</li>
								<li>
									<!-- ACERCA DE --> <a href="#about"> ACERCA DE </a>
								</li>
								<li>
									<!-- WORK --> <a href="#work"> SERVICIOS </a>
								</li>
								<li>
									<!-- TEAM --> <a href="#team"> NUESTRAS APPS </a>
								</li>
								<li>
									<!-- SERVICES --> <a href="#services"> COTIZA </a>
							</ul>

						</nav>
					</div>

				</div>
			</header>
			<!-- /Top Nav -->

		</div>


		<!-- SLIDER -->
		<section id="slider"
			class="page-header page-header-xlg parallax parallax-5"
			style="background-image: url('assets/imagenes/garage_landing_foto2.png');">
			<div class="overlay dark-5">
				<!-- dark overlay [0 to 9 opacity] -->
			</div>




			<div class="display-table">
				<div class="display-table-cell vertical-align-middle">
					<div class="container">

						<div class="slider-featured-text text-center">
							<h1 class="text-white wow fadeInUp" data-wow-delay="0.4s"></h1>
							<h2 class="weight-300 text-white wow fadeInUp blockquote"
								data-wow-delay="0.8s">
								< Diseñamos Apps que mejoran tu vida />
								<footer>
									<cite>-Garage Coders</cite>
								</footer>
							</h2>

							</br> <a class="btn btn-primary btn-tamano wow fadeInUp "
								data-wow-delay="1s"
								href="desarrolloMobil/nuestras_apps.html">Conoce
								nuestras Apps</a>
							<!--
				<blockquote class="weight-300 text-white wow fadeInUp blockquote">
				"EL DISEÑO NO ES LO SÓLO CÓMO SE VE O CÓMO SE SIENTE, EL DISEÑO ES CÓMO FUNCIONA"
				<footer>Groucho Marx</footer>
			</blockquote>
		-->
						</div>

					</div>
				</div>
			</div>

		</section>
		<!-- /SLIDER -->

		<!-- ABOUT -->
		<section id="about">
			<div class="container">

				<header class="text-center margin-bottom-60">
					<h2>Acerca de Garage Coders</h2>
					<br> <img class="img-responsive"
						src="assets/imagenes/garage_landing_linea.png" alt="" />
				</header>


				<div class="row">

					<!--Garage Coders: Se cambia el valor de line-height: 1.1 en el archivo assets/css/essentials.css, linea 251 -->
					<!--Garage Coders: Se cambia el valor de margin: 0 0 25px 0 en el archivo assets/css/essentials.css, linea 268 -->
					<div class="col-md-6 wow bounceInLeft" data-wnow-delay="0.6s">
						<br>
						<h3 class="text-left">Creamos soluciones móviles que van más
							allá de las expectativas de nuestros clientes</h3>
						<p class="text-justify">Somos un equipo altamente eficaz de
							expertos en diferentes ramos, con experiencia adquirida en
							grandes empresas de desarrollo, hemos trabajado al lado de
							grandes empresas en Silicon Valley y en la Ciudad de México,
							comprendemos las necesidades de una empresa y nos ajustamos a
							ellas, creamos productos escalables y de alto impacto, y atacamos
							las tendencias tecnológicas con las mejores tecnologías, para
							todas las plataformas.</p>
						<center>
							<a
								class="btn btn-lg btn btn-xlg btn-default btn-bordered size-20"
								href="desarrolloMobil/nuestras_apps.html">Conoce
								nuestras Apps</a>
						</center>
					</div>

					<div class="col-md-6 pull-right">
						<div class="col-xs-6 wow bounceIn" data-wnow-delay="0.6s">
							<img class="" src="assets/imagenes/garage_landing_proceso.png"
								alt="">
							<h4 class="text-justify">El mejor proceso</h4>
							<p class="text-justify">Nuestra metodología ágil nos permite desarrollar proyectos con grandes resultados.</p>
						</div>
						<div class="col-xs-6 wow bounceIn" data-wnow-delay="0.5s">
							<img class="" src="assets/imagenes/garage_landing_creativos.png"
								alt="">
							<h4 class="text-justify">Somos Creativos</h4>
							<p class="text-justify">Basado en las necesidades de nuestros clientes ,
								trabajamos para obtener soluciones creativas y optimas, ofreciendo así una experiencia de usuario satisfactoria.</p>
						</div>
						<div clas=row>

							<div class="col-xs-6 wow bounceIn" data-wnow-delay="0.4s">
								<img class=""
									src="assets/imagenes/garage_landing_enterprice_friendly.png"
									alt="">
								<h4 class="text-justify">Enterprise Friendly</h4>
								<p class="text-justify">Lorem Ipsum es simplemente el texto
									de relleno de las imprentas y archivos de texto.</p>
							</div>
							<div class="col-xs-6 wow bounceIn" data-wnow-delay="0.3s">
								<img class="" src="assets/imagenes/garage_landing_equipo.png"
									alt="">
								<h4>Trabajamos en Equipo</h4>
								<p class="text-justify">Somos un equipo de profesionales comprometidos a ofrecerte la mejor solución.</p>
							</div>
						</div>
					</div>
				</div>

			</div>
	</div>
	</section>
	<!-- /ABOUT -->

	<!-- VISION/SKILL/SPECIAL -->
	<!-- /VISION/SKILL/SPECIAL -->



	<!-- WORK -->
	<section id="work">
		<div class="container">

			<header class="text-center margin-bottom-60">
				<h2>Nuestros Servicios</h2>
				<br> <img class="img-responsive"
					src="assets/imagenes/garage_landing_linea.png" alt="" />
			</header>

			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img
						src="assets/imagenes/garage_landing_servicios_desarrollo_apps.png">
				</div>
				<div class="col-xs-8">
					<h4>DESARROLLO DE APPS</h4>
					<p class="text-justify">Construimos desarrollos a la medida y
						soluciones que involucren el uso de tecnologías móviles como lo es
						tu teléfono, con más de 22 desarrollos nacionales e
						internacionales durante el último año, en sectores públicos y
						privados, manejando desde tecnologías tradicionales hasta
						tecnologías de última generación.</p>
				</div>
			</div>
			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img
						src="assets/imagenes/garage_landing_servicios_desarrollo_servicios_web.png">
				</div>
				<div class="col-xs-8">
					<h4>DESARROLLO WEB Y BACKEND</h4>
					<p class="text-justify">Todos los sistemas informáticos
						requieren de una infraestructura que los soporte, las Apps no son
						la excepción, por lo tanto desarrollamos sistemas con un Backend
						robusto que incluya los más altos estándares de seguridad y
						calidad.</p>
				</div>
			</div>
			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img src="assets/imagenes/garage_landing_servicios_diseno.png">
				</div>
				<div class="col-xs-8">
					<h4>DISEÑO DE INTERFACES Y EXPERIECNIA DE USUARIO</h4>
					<p class="text-justify">El proceso de mejorar la satisfacción a
						través de la usabilidad de los productos que desarrollamos es una
						pieza angular de nuestros desarrollos, la facilidad de uso e
						interacción que acorten la brecha entre el usuario y la App además
						de generar diseños ergonómicos y hermosos a la vista.</p>
				</div>
			</div>
			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img
						src="assets/imagenes/garage_landing_servicios_desarrollo_servicios_juegos.png">
				</div>
				<div class="col-xs-8">
					<h4>DESARROLLO DE JUEGOS</h4>
					<p class="text-justify">Como una vertical dentro del desarrollo
						de aplicaciones, hemos encontrado que una de las mejores formas de
						aprender es jugando, por lo cual apoyados de conocimientos sólidos
						de marketing desarrollamos estrategias innovadoras de publicidad,
						en especial BTL, aplicando nuevos conceptos publicitarios,
						impactando y generando emociones, haciendo que el público objetivo
						recuerde su marca a esto lo llamamos. “Advergaming”.</p>
				</div>
			</div>
			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img
						src="assets/imagenes/garage_landing_servicios_servicios_qa.png">
				</div>
				<div class="col-xs-8">
					<h4>QUALITY ASSURANCE</h4>
					<p class="text-justify">La calidad está enfocada principalmente
						en la utilidad que tendrá el producto de software para el usuario,
						la utilidad está definida en sus necesidades. Enfocamos este
						concepto durante todo el proceso de desarrollo de las Apps
						generando productos con las siguientes características.</p>
				</div>
			</div>
			<div class="row wow bounceInUp" data-wnow-delay="0.4s">

				<div class="col-xs-4 col-md-push-2">
					<img src="assets/imagenes/garage_landing_servicios_project.png">
				</div>
				<div class="col-xs-8">
					<h4>PROJECT MANAGEMENT</h4>
					<p class="text-justify">Utilizamos el project management y
						proceso de PMI (Project Management Institute) como una disciplina
						que abarca la organización, el planteamiento, la motivación y el
						control de los recursos con la finalidad de alcanzar los objetivos
						propuestos y el éxito en todos nuestros desarrollos.</p>
				</div>
			</div>

			<!-- CONTACT US -->
			<!-- /CONTACT US -->

		</div>
	</section>
	<!-- /WORK -->


	<!----slider revolution------------------------------------------------------------------------------------------------------>

	<!-- REVOLUTION SLIDER -->
	<section id="team">
		<div
			class="slider fullwidthbanner-container roundedcorners margin-bottom-100">
			<!--
							Navigation Styles:

								data-navigationStyle="" theme default navigation

								data-navigationStyle="preview1"
								data-navigationStyle="preview2"
								data-navigationStyle="preview3"
								data-navigationStyle="preview4"

							Bottom Shadows
								data-shadow="1"
								data-shadow="2"
								data-shadow="3"

							Slider Height (do not use on fullscreen mode)
								data-height="300"
								data-height="350"
								data-height="400"
								data-height="450"
								data-height="500"
								data-height="550"
								data-height="600"
								data-height="650"
								data-height="700"
								data-height="750"
								data-height="800"

							Available Classes:
								.thumb-small
								.thumb-large
						-->
			<div class="fullwidthbanner thumb-small" data-height="600"
				data-navigationStyle="">
				<ul class="hide">

					<!-- Primer  SLIDE  -->
					<li data-transition="random" data-slotamount="7"
						data-masterspeed="300" data-saveperformance="off"
						data-thumb="assets/imagenes/banner/banner_mini_my_suite.jpg">
						<!-- imagen pequeña de muestra de carrucel -->
						<img src="assets/imagenes/banner/1x1.png"
						data-lazyload="assets/imagenes/banner/banner_my_suite.jpg" alt=""
						data-bgfit="cover" data-bgposition="left top"
						data-bgrepeat="no-repeat" />
					<!-- imagen grande de muestra de carrucel --> <!-- Apartir de a qui se colo ca el texto y se da formato a los bloques que contendran el texto -->
						<div class="tp-caption sft" data-x="right"
							data-hoffset="-70" data-y="87" data-speed="750" data-start="1100"
							data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 3; max-width: auto; max-height: auto; white-space: nowrap;"><img src="assets/imagenes/banner/banner_logo_my_suite.png"/></div>


						<div class="tp-caption sfb" data-x="right"
							data-hoffset="20" data-y="228" data-speed="750"
							data-start="2000" data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 6; max-width: auto; max-height: auto; white-space: nowrap; ">Herramienta que te permite emitir comprobantes tales<br>
							 como facturas , de igual forma te permite visualizar <br> tus comprobantes emitidos y recibidos.</div>
              <br> <br> <br> <br>

							<div class="tp-caption block_black sft" data-x="right"
								data-hoffset="-20" data-y="325" data-speed="750"
								data-start="2300" data-easing="easeOutExpo" data-splitin="none"
								data-splitout="none" data-elementdelay="0.1"
								data-endelementdelay="0.1" data-endspeed="300"
								style="z-index: 7; max-width: auto; max-height: auto; white-space: nowrap;">
								<center>
									<a
										class="btn btn-lg btn btn-xlg btn-default btn-bordered size-20"
										href="desarrolloMobil/nuestras_apps.html">
										Conoce nuestras Apps
									</a>
								</center>
							</div>
					</li>


					<!--Hasta aqui termina el txto acolocar y las imagenes del primer slider  -->

					<!-- Segundo  SLIDE  -->
					<li data-transition="random" data-slotamount="7"
						data-masterspeed="300" data-saveperformance="off"
						data-thumb="assets/imagenes/banner/banner_mini_contalisto.jpg">
						<!-- A qui esta muestra de imagen pequeña   --> <!-- la imagen que marca como 1x1 solo es para indice no es necesaria-->
						<img src="assets/imagenes/banner/1x1.png"
						data-lazyload="assets/images/demo/1200x800/banner_contalisto.jpg" alt=""
						data-bgfit="cover" data-bgposition="center center"
						data-bgrepeat="no-repeat" /> <!--  imagen grande  del segundo slider -->

						<div class="tp-caption mediumlarge_light_white lft tp-resizeme"
							data-x="left" data-hoffset="-30" data-y="85" data-speed="1000"
							data-start="1200" data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 4; max-width: auto; max-height: auto; white-space: nowrap;"><img src="assets/imagenes/banner/banner_logo_contalisto.png"/></div>

						<div class="tp-caption sfl tp-resizeme" data-x="-15"
							data-y="220" data-speed="750" data-start="1900"
							data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 5; max-width: auto; max-height: auto; white-space: nowrap;">Captura fotografías de tus tickets y obtienes tu factura<br>
							 hecha, también te hace un el balance de tus gastos<br>
							 e ingresos y del monto aproximado que tendrás <br>
							 que pagar de impuestos al mes.
						 </div>

						<div class="tp-caption block_black sfb tp-resizeme" data-x="20"
							data-y="340" data-speed="750" data-start="2500"
							data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 7; max-width: auto; max-height: auto; white-space: nowrap;">
							<center>
								<a
									class="btn btn-lg btn btn-xlg btn-default btn-bordered size-20"
									href="desarrolloMobil/nuestras_apps.html">
									Conoce nuestras Apps
								</a>
							</center>
						</div>

					</li>


					<!-- Tercer  SLIDE  -->
					<li data-transition="random" data-slotamount="7"
						data-masterspeed="300" data-saveperformance="off"
						data-thumb="assets/imagenes/banner/banner_mini_burp.jpg">
						<!-- A qui esta muestra de imagen pequeña   --> <!-- la imagen que marca como 1x1 solo es para indice no es necesaria-->
						<img src="assets/images/1x1.png"
						data-lazyload="assets/imagenes/banner/banner_burp.jpg" alt=""
						data-bgfit="cover" data-bgposition="center center"
						data-bgrepeat="no-repeat" /> <!--  imagen grande  del tercer slider -->

						<div class="tp-caption mediumlarge_light_white lft tp-resizeme"
							data-x="left" data-hoffset="60" data-y="85" data-speed="1000"
							data-start="1200" data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 4; max-width: auto; max-height: auto; white-space: nowrap;"><img src="assets/imagenes/banner/banner_logo_burp.png"/></div>

						<div class="tp-caption sfl tp-resizeme" data-x="70"
							data-y="216" data-speed="750" data-start="1900"
							data-easing="easeOutExpo" data-splitin="none"
							data-splitout="none" data-elementdelay="0.1"
							data-endelementdelay="0.1" data-endspeed="300"
							style="z-index: 5; max-width: auto; max-height: auto; white-space: nowrap;">Aplicación que permite realizar compras de bebidas<br>
							 alcohólicas con servicio a domicilio.</div>

							 <div class="tp-caption block_black sfb tp-resizeme" data-x="100"
	 							data-y="290" data-speed="750" data-start="2500"
	 							data-easing="easeOutExpo" data-splitin="none"
	 							data-splitout="none" data-elementdelay="0.1"
	 							data-endelementdelay="0.1" data-endspeed="300"
	 							style="z-index: 7; max-width: auto; max-height: auto; white-space: nowrap;">
								<center>
									<a
										class="btn btn-lg btn btn-xlg btn-default btn-bordered size-20"
										href="desarrolloMobil/nuestras_apps.html">
										Conoce nuestras Apps
									</a>
								</center>

							</div>


							</center>

						</div>
					</li>


				</ul>

				<!-- <div class="tp-bannertimer">-->
				<!-- progress bar -->
				<!--  </div> -->
			</div>
		</div>

	</section>
	<!-- /REVOLUTION SLIDER -->

	<!-- ////------------------------------------------------------------------------------------------------------------------ -->


	<!-- TEAM -->

	<!-- --------------------------  Comentariado por sustitución de Carrusel
						<section id="team">
							<div class="container">

								<header class="text-center margin-bottom-60">
									<h2>Nuestras Apps</h2>
									<br>
									<img class="img-responsive" src="assets/imagenes/garage_landing_linea.png" alt="" />
								</header>

								<div class="row">


									<div class="col-md-6">

										<img class="img-responsive" src="assets/imagenes/apps/garage_landing_app_1.png" alt="" />


									</div>

									<div class="col-md-5">
										<img class="img-responsive" src="assets/imagenes/apps/garage_landing_titulo_app_1.png" alt="" /><br><br>
										<p clas="text-justify ">Captura fotografías de tus tickets y obtienes tu factura hecha, también te hace un el balance
											de tus gastos e ingresos y del monto aproximado que tendrás que pagar de impuestos al mes.</p>
											<center><a href="desarrolloMobil/desarrolloMobilConoceNuestrasApps.html" class="btn btn-xlg btn-default btn-bordered size-20">
												Conoce nuestras Apps
											</a></center>

										</div>

									</div>

								</div>
						</section>   ---------------------- -->



	<!-- /TEAM -->


	<!-- SERVICES -->
	<section id="services">
		<div class="container">
			<header class="text-center margin-bottom-60">
				<h2>Haz una Cotización</h2>
				<br> <img class="img-responsive"
					src="assets/imagenes/garage_landing_linea.png" alt="" />
			</header>

			<h3 class="text-center margin-bottom-60">
				¿Tienes un proyecto y estas interesado en desarrollarlo? <br>
				Escríbenos nos encantaría ayudarte a llevarlo a cabo.
			</h3>

			<!-- -->
			<div class="col-md-12 col-centered">
				<a id="botonCotizacion" class="btn btn-green btn-lg wow "
					data-wow-delay="1s">Solicita una Cotización</a>
			</div>
			<br>

			<section id="formularioOculto" class="ocultarElemento">
				<div class="container">

					<div class="row">

						<!-- FORM -->
						<div class="col-md-12">



							<!-- Alert Success -->
							<div id="alert_success"
								class="alert alert-success margin-bottom-30">
								<button type="button" class="close" data-dismiss="alert"
									aria-hidden="true">&times;</button>
								<strong>Gracias </strong> Su mensaje a sido enviado
								correctamente!
							</div>
							<!-- /Alert Success -->


							<!-- Alert Failed -->
							<div id="alert_failed"
								class="alert alert-danger margin-bottom-30">
								<button type="button" class="close" data-dismiss="alert"
									aria-hidden="true">&times;</button>
								<strong>[SMTP] Error!</strong> ¡Error de servidor interno!
							</div>
							<!-- /Alert Failed -->

							<!--
							<!-- Alert Mandatory
							<div id="alert_mandatory"
								class="alert alert-danger margin-bottom-30">
								<button type="button" class="close" data-dismiss="alert"
									aria-hidden="true">&times;</button>
								<strong>Disculpa</strong> Es necesario completar todos los
								campos obligatorios ( * )!
							</div>
							<!-- /Alert Mandatory    -->

							<!-- Comineza formulario -->
							<form action="php/contact.php" method="post"
								enctype="multipart/form-data" id="folmularioGarage" >
								<fieldset>
									<input type="hidden" name="action" value="contact_send" />

									<div class="row">
										<div class="form-group">
											<div class="col-md-4">
												<label for="contact:Nombre" class="fuente">Nombre
													Completo *</label> <input type="text" value=""
													class="formulario_celda digits" name="contact[nombre]"
													id="nombre" placeholder="Rafael Dominguez">
											</div>
											<div class="col-md-4">
												<label for="contact:Email" class="fuente">Dirección
													de E-mail *</label> <input required type="email" value=""
													class="formulario_celda" name="contact[email][required]"
													id="email" placeholder="ejemplo@ejemplo.com">
											</div>
											<div class="col-md-4">
												<label for="contact:Telefono" class="fuente">Numero
													Teléfonico *</label> <input required type="text" value=""
													class="formulario_celda" name="contact[telefono][required]"
													id="contact:telefono" placeholder="555-555-555">
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-md-8">
												<label for="contact:Nombre de la empresa" class="fuente">Nombre
													de tu empresa *</label> <input required type="text" value=""
													class="formulario_celda"
													name="contact[nombre empresa][required]"
													id="contact:nombreempresa" placeholder="Company">
											</div>

										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-md-12">
												<label for="contact:Descripción" class="fuente">Describenos
													tu aplición *</label>
												<textarea required maxlength="10000" rows="8"
													class="formulario_celda"
													name="contact[descripción]][required]"
													id="contact:descripcion" form="folmularioGarage"
													placeholder="Descripción"></textarea>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="form-group">
											<div class="col-md-12">
												<label for="contact:attachment" class="fuente">Plataforma
													/ Dispositivos *</label> <label class="radio"> <input
													type="checkbox" name="contact[android Smartphone]"
													id="contact:androidSmart"> <i></i> Android
													smartphone
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[android Tablet]"
													id="contact:androidTablet"> <i></i> Android
													Tablet
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[android Wereable]"
													id="contact:androidWereable"> <i></i> Android
													Wereable
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[apple Tv]"
													id="contact:appleTv"> <i></i> Apple TV
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[apple Watch]"
													id="contact:appleWatch"> <i></i> Apple Watch
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[ipad]" id="contact:ipad">

													<i></i> ipad
												</label> <br> <label class="radio"> <input
													type="checkbox" name="contact[iphone]" id="contact:iphone">

													<i></i> iphone
												</label>

											</div>
										</div>
									</div>
									<!-- select -->
									<div class="row">
										<div class="col-md-8">
											<label for="contact:presupuesto" class="fuente">Elige
												un presupuesto</label>
											<div class="fancy-form fancy-form-select">
												<select required class="form-control"
													name="contact[presupuesto]" id="contact:presupuesto">
													<option value="">--</option>
													<option value="10,000 - 30,000">10,000 - 30,000</option>
													<option value="30,000 - 60,000">30,000 - 60,000</option>
													<option value="60,000 - 90,000">60,000 - 90,000</option>
													<option value="100,000 o más">100,000 o más</option>
												</select> <i class="fancy-arrow"></i>
											</div>
										</div>
									</div>


									<div class="row">
										<div class="col-md-8">
											<label for="contact:fechainicio" class="fuente">¿Cuando
												quieres comenzar el proyecto? *</label>
												<input required type="text"
												id="fecha-inicio" class="formulario_celda "
												name="contact[fecha-inicio]"

												placeholder="dd-mm-aaaa">

										</div>
									</div>
									<div class="row">
										<div class="col-md-8">
											<label for="contact:fechafin" class="fuente">¿Cuando
												quieres terminar el proyecto? *</label>
												 <input required type="text"
												id="fecha-fin" class="formulario_celda "
												name="contact[fecha-fin]"  placeholder="dd-mm-aaaa">

										</div>
									</div>



								</fieldset>

								<div class="row">
									<div class="col-md-6 "></div>
									<div class="col-md-6 col-md-push-3">
										<button id="botonOculta" type="button"
											class="btn-form btn btn-danger">
											<i class="fa fa-times"></i> Cancelar
										</button>
										<button type="submit" class="btn-form btn btn-primary">
											<i class="fa fa-check"></i> Enviar
										</button>

									</div>
								</div>





							</form>

						</div>
						<!-- /FORM -->

					</div>

				</div>
			</section>
			<!-- / -->


		</div>
	</section>


	<!-- END SERVICES -->






	<!---------------------------------------------------------------------------------------->

	<!-- FOOTER -->
	<?php
	include 'footer.php';
	?>
	<!-- /FOOTER -->

	</div>
	<!-- /wrapper -->


	<!-- SCROLL TO TOP -->
	<a href="#" id="toTop"></a>


	<!-- PRELOADER -->
	<div id="preloader">
		<div class="inner">
			<span class="loader"></span>
		</div>
	</div>
	<!-- /PRELOADER -->


	<!-- JAVASCRIPT FILES -->
	<script type="text/javascript">var plugin_path = 'assets/plugins/';</script>
	<script type="text/javascript"
		src="assets/plugins/jquery/jquery-2.1.4.min.js"></script>

	<script type="text/javascript" src="assets/js/scripts.js"></script>

	<!-- REVOLUTION SLIDER -->
	<script type="text/javascript"
		src="assets/plugins/slider.revolution/js/jquery.themepunch.tools.min.js"></script>
	<script type="text/javascript"
		src="assets/plugins/slider.revolution/js/jquery.themepunch.revolution.min.js"></script>
	<script type="text/javascript"
		src="assets/js/view/demo.revolution_slider.js"></script>

	<!-- PAGELEVEL SCRIPTS -->
	<script type="text/javascript" src="assets/js/contact.js"></script>

	<!--
							GMAP.JS
							http://hpneo.github.io/gmaps/
						-->
	<script type="text/javascript"
		src="//maps.google.com/maps/api/js?sensor=true"></script>
	<script type="text/javascript" src="assets/plugins/gmaps.js"></script>

	<script type="text/javascript">



						jQuery(document).ready(function(){




							/* Boton Cotización*/
							$("#botonCotizacion").click(function(){

								$("#formularioOculto").show();
							});
							$("#botonOculta").click(function(){

								$("#formularioOculto").hide();
							});
						});

						</script>
	<!-- Jquery validate  -->



	<script type="text/javascript">


        	$( "#folmularioGarage" ).validate({
        		  rules: {
        		    #nombre: {
        		      minlength: 4,
        		      lettersonly: true,
        		      regex:"^[a-zA-Z]+$"
        		    },
                    email: {

        		       minlength: 4,
        		       email:true
	        		      },
	        		      telefono: {

	            		      minlength: 10,
	            		      digits: true
	            		    },
	            		    nombreempresa: {

		            		      minlength: 4
		            		    },
		            		    descripcion: {

			            		      minlength: 4
			            		    },
			            		    presupuesto: {


				            		    },


	        		      fecha-fin: {
        		      required: true
	        		      },
	        		      fecha-inicio: {
        		      required: true
	        		      },

        		  },


        		  messages: {
        			  contact[nombre] : {
        				 required: "Ingrese su nombre",
        				 minlength: jQuery.validator.format("Debe de tener almenos {0} caracteres"),
        				 lettersonly:"Ingrese solo letras"
        			  },
                     email: {
        				 required: "Ingrese Correo Electrónico",
        				 minlength: jQuery.validator.format("Debe de tener almenos {0} numeros"),
        				 email:"Ingres un Correo valido"
        			  },

        			  telefono: {
            		      required: "Ingrese numero telefónico",
            		      minlength: jQuery.validator.format("Debe de tener almenos {0} numeros"),
            		      number: "Ingrese solo numeros"
            		    },
            		    nombreempresa: {
	            		      required: "Ingrse el nombre de la empresa",
	            		      minlength: jQuery.validator.format("Debe de tener almenos {0} numeros")
	            		    },
	            		    descripcion: {
		            		      required: "Ingrese una descripción",
		            		      minlength: jQuery.validator.format("Debe de tener almenos {0} numeros")
		            		    },
		            		    presupuesto: {
			            		      required: "Ingrese un presupuesto"

			            		    },



        			  fecha-fin : {
        				 required: "Ingrese la fecha de Fin"

        			  },

        			  fecha-inicio: {
        				 required: "Ingrese la fecha de Inicio"

        			  }

                    },
                    submitHandler: function() {
                        alert("Formulario enviado");

        		}

        	});

        </script>


	<script>
$(function () {
$("#fecha-inicio").datepicker({
onClose: function (selectedDate) {
$("#fecha-fin").datepicker("option", "minDate", selectedDate);
}
});
$("#fecha-fin").datepicker({
onClose: function (selectedDate) {
$("#fecha-inicio").datepicker("option", "maxDate", selectedDate);
}
});
});
</script>



	<!-- fin jquery validate -->

<script src="dist/jquery.validate.min.js"></script>
	<script src="dist/localization/messages_es.min.js"></script>

	<script src="calendar.min.js"></script>

	<script src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
	<script
		src="http://digitalbush.com/wp-content/uploads/2013/01/jquery.maskedinput-1.3.1.min_.js"
		type="text/javascript"></script>



</body>
</html>
