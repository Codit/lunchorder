import { Component } from '@angular/core';
@Component({
  selector: 'my-app',
  template: `<!-- FullScreen -->
	<div class="intro-header">
		<div class="col-xs-12 text-center abcen1">
			<h1 class="h1_home wow fadeIn" data-wow-delay="0.4s">Lunch Order</h1>
			<h3 class="h3_home wow fadeIn" data-wow-delay="0.6s">Order lunch at your enterprise with ease</h3>
			<ul class="list-inline intro-social-buttons">
				<li><a href="#" class="btn  btn-lg mybutton_cyano wow fadeIn" data-wow-delay="0.8s"><span class="network-name">Login using Company Account</span></a>
				</li>
				<!--<li id="download" ><a href="#downloadlink" class="btn  btn-lg mybutton_standard wow swing wow fadeIn" data-wow-delay="1.2s"><span class="network-name">Order now!</span></a>-->
				<!--</li>-->
			</ul>
		</div>
		<!-- /.container -->
		<div class="col-xs-12 text-center abcen wow fadeIn">
			<div class="button_down ">
				<a class="imgcircle wow bounceInUp" data-wow-duration="1.5s" href="#order"> <img class="img_scroll" src="css/images/icon/circle.png" alt=""> </a>
			</div>
		</div>
	</div>
  

	<!-- NavBar-->
	<nav class="navbar-default" role="navigation">
		<div class="container">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</button>
				<a class="navbar-brand" href="#home">LunchOrder</a>
			</div>

			<div class="collapse navbar-collapse navbar-right navbar-ex1-collapse">
				<ul class="nav navbar-nav">

					<li class="menuItem"><a href="#order">Order</a></li>
					<li class="menuItem"><a href="#balance">Balance</a></li>
					<li class="menuItem"><a href="#badges">Badges</a></li>
					<li class="menuItem"><a href="#contact">Contact</a></li>
				</ul>
			</div>

		</div>
	</nav>

	<!-- Login -->
	<div id="login" class="content-section-b" style="border-top: 0">
		<div class="container">

			<!--
			<div class="col-md-6 col-md-offset-3 text-center wrap_title">
				<h2>Login</h2>
				<p class="lead" style="margin-top:0">Please login to continue</p>
			</div>
			
			<div class="row">
			
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img class="rotate" src="css/images/icon/tweet.svg" alt="Generic placeholder image">
				  <h3>Follow Me</h3>
				  <p class="lead">Epsum factorial non deposit quid pro quo hic escorol. Olypian quarrels et gorilla congolium sic ad nauseum. </p>

				  
				</div>
				
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img  class="rotate" src="css/images/icon/picture.svg" alt="Generic placeholder image">
				   <h3>Gallery</h3>
				   <p class="lead">Epsum factorial non deposit quid pro quo hic escorol. Olypian quarrels et gorilla congolium sic ad nauseum. </p>
				   
				</div>
				
				<div class="col-sm-4 wow fadeInDown text-center">
				  <img  class="rotate" src="css/images/icon/retina.svg" alt="Generic placeholder image">
				   <h3>Retina</h3>
					<p class="lead">Epsum factorial non deposit quid pro quo hic escorol. Olypian quarrels et gorilla congolium sic ad nauseum. </p>
				  
				</div>
				
			</div>
			-->

			<div class="row tworow">

				<div class="col-sm-4  wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/watch.svg" alt="Generic placeholder image">
					<h3>On Time</h3>
					<p class="lead">Orders are sent to the venue @ 9:30 am. Late orders are your own responsibility and should be made by giving them a
						call. </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/map.svg" alt="Generic placeholder image">
					<h3>Venue</h3>
					<p class="lead">Venue placeholder details come here </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

				<div class="col-sm-4 wow fadeInDown text-center">
					<img class="rotate" src="css/images/icon/savings.svg" alt="Generic placeholder image">
					<h3>Payment</h3>
					<p class="lead">Payment is done upfront. Please consult your user profile to add funds to your account. </p>
					<!-- <p><a class="btn btn-embossed btn-primary view" role="button">View Details</a></p> -->
				</div>
				<!-- /.col-lg-4 -->

			</div>
			<!-- /.row -->
		</div>
	</div>

	<!-- Order -->
	<div id="order" class="content-section-a">

		<div class="container">

			<div class="row">

				<div class="col-sm-6 pull-right wow fadeInRightBig">
					<img class="img-responsive " src="css/images/ipad.png" alt="">
				</div>

				<div class="col-sm-6 wow fadeInLeftBig" data-animation-delay="200">
					<h3 class="section-heading">Full Responsive</h3>
					<div class="sub-title lead3">Lorem ipsum dolor sit atmet sit dolor greand fdanrh<br> sdfs sit atmet sit dolor greand fdanrh sdfs</div>
					<p class="lead">
						In his igitur partibus duabus nihil erat, quod Zeno commuta rest gestiret. Sed virtutem ipsam inchoavit, nihil ampliusuma.
						Scien tiam pollicentur, uam non erat mirum sapientiae lorem cupido patria esse cariorem. Quae qui non vident, nihilamane
						umquam magnum ac cognitione.
					</p>

					<p><a class="btn btn-embossed btn-primary" href="#" role="button">View Details</a>
						<a class="btn btn-embossed btn-info" href="#" role="button">Visit Website</a></p>
				</div>
			</div>
		</div>
		<!-- /.container -->
	</div>

	<div class="content-section-b">

		<div class="container">
			<div class="row">
				<div class="col-sm-6 wow fadeInLeftBig">
					<div id="owl-demo-1" class="owl-carousel">
						<a href="css/images/iphone.png" class="image-link">
							<div class="item">
								<img class="img-responsive img-rounded" src="css/images/iphone.png" alt="">
							</div>
						</a>
						<a href="css/images/iphone.png" class="image-link">
							<div class="item">
								<img class="img-responsive img-rounded" src="css/images/iphone.png" alt="">
							</div>
						</a>
						<a href="css/images/iphone.png" class="image-link">
							<div class="item">
								<img class="img-responsive img-rounded" src="css/images/iphone.png" alt="">
							</div>
						</a>
					</div>
				</div>

				<div class="col-sm-6 wow fadeInRightBig" data-animation-delay="200">
					<h3 class="section-heading">Drag Gallery</h3>
					<div class="sub-title lead3">Lorem ipsum dolor sit atmet sit dolor greand fdanrh<br> sdfs sit atmet sit dolor greand fdanrh sdfs</div>
					<p class="lead">
						In his igitur partibus duabus nihil erat, quod Zeno commuta rest gestiret. Sed virtutem ipsam inchoavit, nihil ampliusuma.
						Scien tiam pollicentur, uam non erat mirum sapientiae lorem cupido patria esse cariorem. Quae qui non vident, nihilamane
						umquam magnum ac cognitione.
					</p>

					<p><a class="btn btn-embossed btn-primary" href="#" role="button">View Details</a>
						<a class="btn btn-embossed btn-info" href="#" role="button">Visit Website</a></p>
				</div>
			</div>
		</div>
	</div>

	<div class="content-section-a">

		<div class="container">

			<div class="row">

				<div class="col-sm-6 pull-right wow fadeInRightBig">
					<img class="img-responsive " src="css/images/doge.png" alt="">
				</div>

				<div class="col-sm-6 wow fadeInLeftBig" data-animation-delay="200">
					<h3 class="section-heading">Font Awesome & Glyphicon</h3>
					<p class="lead">A special thanks to Death to the Stock Photo for providing the photographs that you see in this template.
					</p>

					<ul class="descp lead2">
						<li><i class="glyphicon glyphicon-signal"></i> Reliable and Secure Platform</li>
						<li><i class="glyphicon glyphicon-refresh"></i> Everything is perfectly orgainized for future</li>
						<li><i class="glyphicon glyphicon-headphones"></i> Attach large file easily</li>
					</ul>
				</div>
			</div>
		</div>

	</div>

	<!-- Balance -->
	<div id="balance" class="content-section-b">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center wrap_title ">
					<h2>Balance</h2>
					<p class="lead" style="margin-top:0">Check your balance and other options.</p>
				</div>
			</div>

		</div>


	</div>

	<div class="content-section-c ">
		<div class="container">
			<div class="row">
				<div class="col-md-6 col-md-offset-3 text-center white">
					<h2>Get Reminders</h2>
					<p class="lead" style="margin-top:0">Never miss an order.</p>
				</div>
				<div class="col-md-6 col-md-offset-3 text-center">
					<div class="mockup-content">
						<div class="morph-button wow pulse morph-button-inflow morph-button-inflow-1">
							<button type="button "><span>Subscribe to our Newsletter</span></button>
							<div class="morph-content">
								<div>
									<div class="content-style-form content-style-form-4 ">
										<h2 class="morph-clone">Subscribe to our Newsletter</h2>
										<form>
											<p><label>Your Email Address</label><input type="text" /></p>
											<p><button>Remind me</button></p>
										</form>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>>
		</div>
	</div>

	<!-- Badges -->
	<div id="badges" class="content-section-a">
		<div class="container">
			<div class="row">

				<div class="col-md-6 col-md-offset-3 text-center wrap_title">
					<h2>Badges</h2>
					<p class="lead" style="margin-top:0">Earn badges along the way.</p>
				</div>

				<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-desktop fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Bootstrap </h3>
							<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>
						</div>
					</div>
				</div>
				<div class="col-sm-6 block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-picture-o fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Owl-Carousel </h3>
							<p> Nullam mo arcu ac molestie scelerisqu vulputate, molestie ligula gravida, tempus ipsum.</p>
						</div>
					</div>
				</div>
			</div>

			<div class="row tworow">
				<div class="col-sm-6  block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-magic fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Codrops </h3>
							<p> Lorem ipsum dolor sit ametconsectetur adipiscing elit. Suspendisse orci quam. </p>
						</div>
					</div>
				</div>
				<div class="col-sm-6 block wow bounceIn">
					<div class="row">
						<div class="col-md-4 box-icon rotate">
							<i class="fa fa-heart fa-4x "> </i>
						</div>
						<div class="col-md-8 box-ct">
							<h3> Lorem Ipsum</h3>
							<p> Nullam mo arcu ac molestie scelerisqu vulputate, molestie ligula gravida, tempus ipsum.</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>

	<!-- Banner Download -->
	<div id="downloadlink" class="banner">
		<div class="container">
			<div class="row">
				<div class="col-md-8 col-md-offset-4 text-right wrap_title">
					<h2>Enjoy your lunch</h2>
					<p class="lead" style="margin-top:0">Tweet your order</p>
				</div>
			</div>
		</div>
	</div>

	<!-- Contact -->
	<div id="contact" class="content-section-a">
		<div class="container">
			<div class="row">

				<div class="col-md-6 col-md-offset-3 text-center wrap_title">
					<h2>Contact Us</h2>
					<p class="lead" style="margin-top:0">A special thanks to Death.</p>
				</div>

				<form role="form" action="" method="post">
					<div class="col-md-6">
						<div class="form-group">
							<label for="InputName">Your Name</label>
							<div class="input-group">
								<input type="text" class="form-control" name="InputName" id="InputName" placeholder="Enter Name" required>
								<span class="input-group-addon"><i class="glyphicon glyphicon-ok form-control-feedback"></i></span>
							</div>
						</div>

						<div class="form-group">
							<label for="InputEmail">Your Email</label>
							<div class="input-group">
								<input type="email" class="form-control" id="InputEmail" name="InputEmail" placeholder="Enter Email" required>
								<span class="input-group-addon"><i class="glyphicon glyphicon-ok form-control-feedback"></i></span>
							</div>
						</div>

						<div class="form-group">
							<label for="InputMessage">Message</label>
							<div class="input-group">
								<textarea name="InputMessage" id="InputMessage" class="form-control" rows="5" required></textarea>
								<span class="input-group-addon"><i class="glyphicon glyphicon-ok form-control-feedback"></i></span>
							</div>
						</div>

						<input type="submit" name="submit" id="submit" value="Submit" class="btn wow tada btn-embossed btn-primary pull-right">
					</div>
				</form>

				<hr class="featurette-divider hidden-lg">
				<div class="col-md-5 col-md-push-1 address">
					<address>
						<h3>Office Location</h3>
						<p class="lead"><a href="https://www.google.com/maps/preview?ie=UTF-8&q=The+Pentagon&fb=1&gl=us&hq=1400+Defense+Pentagon+Washington,+DC+20301-1400&cid=12647181945379443503&ei=qmYfU4H8LoL2oATa0IHIBg&ved=0CKwBEPwSMAo&safe=on">The Pentagon<br>
					Washington, DC 20301</a><br> Phone: XXX-XXX-XXXX<br> Fax: XXX-XXX-YYYY</p>
					</address>

					<h3>Social</h3>
					<li class="social">
						<a href="#"><i class="fa fa-facebook-square fa-size"> </i></a>
						<a href="#"><i class="fa  fa-twitter-square fa-size"> </i> </a>
						<a href="#"><i class="fa fa-google-plus-square fa-size"> </i></a>
						<a href="#"><i class="fa fa-flickr fa-size"> </i> </a>
					</li>
				</div>
			</div>
		</div>
	</div>



	<footer>
		<div class="container">
			<div class="row">
				<div class="col-md-7">
					<h3 class="footer-title">Follow Me!</h3>
					<p>Vuoi ricevere news su altri template?<br/> Visita Andrea Galanti.it e vedrai tutte le news riguardanti nuovi Theme!<br/>						Go to: <a href="http://andreagalanti.it" target="_blank">andreagalanti.it</a>
					</p>

					<!-- LICENSE -->
					<a rel="cc:attributionURL" href="http://www.andreagalanti.it/flatfy" property="dc:title">Flatfy Theme </a> by
					<a rel="dc:creator" href="http://www.andreagalanti.it" property="cc:attributionName">Andrea Galanti</a> is licensed
					to the public under
					<BR>the <a rel="license" href="http://creativecommons.org/licenses/by-nc/3.0/it/deed.it">Creative
		   Commons Attribution 3.0 License - NOT COMMERCIAL</a>.


				</div>
				<!-- /col-xs-7 -->

				<div class="col-md-5">
					<div class="footer-banner">
						<h3 class="footer-title">Flatfy Theme</h3>
						<ul>
							<li>12 Column Grid Bootstrap</li>
							<li>Form Contact</li>
							<li>Drag Gallery</li>
							<li>Full Responsive</li>
							<li>Lorem Ipsum</li>
						</ul>
						Go to: <a href="http://andreagalanti.it/flatfy" target="_blank">andreagalanti.it/flatfy</a>
					</div>
				</div>
			</div>
		</div>
		< </footer>`})
export class AppComponent { }