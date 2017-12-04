/* ================================================
----------- Geass ---------- */
(function ($) {
    "use strict";
    var Geass = {
        initialised: false,
        mobile: false,
        container: $('#portfolio-item-container'),
        portfolioItemsAnimated: false,
        init: function () {

            if (!this.initialised) {
                this.initialised = true;
            } else {
                return;
            }

            // Call Geass Functions
            this.queryLoad();
            this.checkMobile();
            this.videoBg();
            this.checkPlaceholder();
            this.fitTexts();
            this.scrollAnimations();
            this.homeSectionHeight();
            this.menuScrollToAnimation();
            this.stickyMenu();
            this.collapseIcons();
            this.countdowns();
            this.owlCarousels();
            this.singlePortfolioOwl();
            this.scrollToTopAnimation();
            this.scrollToClass();
            this.selectBox();
            this.bootstrapSwitch();
            this.tooltip();
            this.popover();
            this.countTo();
            this.progressBars();
            this.registerKnob();
            this.prettyPhoto();
            this.flickerFeed();
            this.contactForm();
            this.parallax();
            this.twitterFeed();
            this.contactFormFixes();

            /* Gallery pages Animation of gallery elements and isotope filter plugin*/
            var self = this;
            if (typeof imagesLoaded === 'function') {
                imagesLoaded(self.container, function () {
                    self.calculateWidth();
                    self.isotopeActivate();
                    self.scrollTriggerforPortfolioAnim();
                    self.prettyPhoto();
                    // hover animation
                    self.hoverAnimation();
                    // recall for plugin support
                    self.isotopeFilter();
                    // load portfolio projects
                    self.openProject();
                });

            }

        },
        queryLoad: function () {
            var self = this;
            if ($.fn.queryLoader2) {
                $("body").queryLoader2({
                    barColor: "#f8d61b",
                    backgroundColor: "#fff",
                    percentage: true,
                    barHeight: 5,
                    minimumTime: 700,
                    fadeOutTime: 200,
                    onComplete: function () {
                        /* fadeout then remove loader*/
                        /* You can change width-height to achieve different animations after load*/
                        $(".geass-loader-overlay").animate({ 'height': 'hide', 'opacity': 0.25 }, 460, function () {
                            $(this).remove();
                        });
                    }
                });
            }
        },
        checkMobile: function () {
            /* Mobile Detect*/
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                this.mobile = true;
            } else {
                this.mobile = false;
            }
        },
        videoBg: function () {
            // for index3.html if mobile add class for bg "homebg"
            // This plugin doesnt work on mobile devices
            if (this.mobile && $('#home').hasClass('videobg')) {
                $('#home').addClass('homebg');
            } else {
                if ($.fn.mb_YTPlayer) {
                    $(".player").mb_YTPlayer();
                } else {
                    return;
                }
            }
        },
        checkPlaceholder: function () {
            /* Check for placeholder support */
            $.support.placeholder = (function () {
                var i = document.createElement('input');
                return 'placeholder' in i;
            })();

            // if Placeholder is not supported call plugin
            if (!$.support.placeholder && $.fn.placeholder) {
                $('input, textarea').placeholder();
            }
        },
        fitTexts: function () {
            /* Better responsive texts with fittext plugin */
            if ($.fn.fitText) {

                $('.section-title').fitText(1.3, { minFontSize: '40px', maxFontSize: '75px' });
                $('.parallax-title').fitText(1.4, { minFontSize: '22px', maxFontSize: '36px' });

                $('.page-title').fitText(1.2, { minFontSize: '50px', maxFontSize: '120px' });
            }

        },
        homeSectionHeight: function () {
            // Boxed version fix home section's height
            if ($('#wrapper').hasClass('boxed') || $('#wrapper').hasClass('boxed-long')) {
                var winHeight = $(window).height();
                $('#home').height(winHeight)
            } else {
                return;
            }
        },
        stickyMenu: function () {
            // Stickymenu with waypoint and waypoint sticky plugins
            if ($.fn.waypoint && $(window).outerWidth() > 767) {

                var calcOffset,
					fixedClass = 'navbar-fixed-top';

                // To calculate offset for fixed menu
                if ($('#header').find('.navbar').hasClass('navbar-transparent')) {
                    calcOffset = -300;
                } else {
                    calcOffset = 0;
                }

                /* Add Header fixed-bottom to make nav menu fixed bottom*/
                if ($('#header').hasClass('fixed-bottom')) {
                    fixedClass = 'navbar-fixed-bottom'
                }

                $('#header').find('.navbar').waypoint('sticky', {
                    stuckClass: fixedClass + ' fixed-animated',
                    offset: calcOffset
                });
            }
        },
        destroyStickyMenu: function () {
            // Destroy Stickymenu for smaller devices
            if ($.fn.waypoint && $(window).width() < 767) {
                $('#header').find('.navbar').waypoint('unsticky');
            }
        },
        twitterFeed: function () {
            // Twitter Feed
            if ($.fn.tweet && $('.twitter_feed').length) {
                $('.twitter_feed').tweet({
                    modpath: './js/twitter/',
                    avatar_size: 48,
                    count: 4,
                    query: "wrapbootstrap", // write feed query here
                    loading_text: "searching twitter...",
                    join_text: "",
                    template: "{join}{text}{time}"
                    /* etc... */
                });

                $('.tweet_list').owlCarousel({
                    singleItem: true,
                    slideSpeed: 600,
                    autoPlay: 8200,
                    stopOnHover: true,
                    navigation: true,
                    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                    pagination: false,
                    responsive: true,
                    autoHeight: false,
                    transitionStyle: "backSlide"
                });
            }
        },
        collapseIcons: function () {
            // Change accordion/collapse icons with adding class
            // Change accordion/collapse icons with adding class
            $('.panel').each(function () {
                var $this = $(this),
					accordionBtn = $this.find('.accordion-btn'),
					accordionBody = $this.find('.accordion-body');

                if (accordionBtn.length) {
                    accordionBody.on('shown.bs.collapse', function () {

                        if (!accordionBtn.hasClass('open')) {
                            accordionBtn.addClass('open');
                        }

                    }).on('hidden.bs.collapse', function () {
                        if (accordionBtn.hasClass('open')) {
                            accordionBtn.removeClass('open');
                        }

                    });
                }

            });
        },
        countdowns: function () {
            // Countdown plugin Used for event cauntdowns
            if ($.fn.countdown) {

                // countdown  - event.single.html
                var eventCoundown = new Date();
                eventCoundown = new Date(eventCoundown.getFullYear() + 1, 3 - 1, 1);
                $('#event-countdown').countdown({ until: eventCoundown });
            }
        },
        checkSupport: function (elemname, pluginname) {
            /* Simple check element and plugin */
            return (elemname.length && pluginname) ? true : false;
        },
        owlCarousels: function () {
            var self = this;

            /* testimonials carousel - index-two.html */
            var testimonialsCarousel = $('.owl-carousel.testimonials-carousel');
            if (self.checkSupport(testimonialsCarousel, $.fn.owlCarousel)) {
                testimonialsCarousel.owlCarousel({
                    singleItem: true,
                    slideSpeed: 600,
                    autoPlay: 9000,
                    stopOnHover: true,
                    navigation: true,
                    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                    pagination: false,
                    responsive: true,
                    autoHeight: false,
                    transitionStyle: "backSlide"
                });
            }

        },
        singlePortfolioOwl: function () {
            /* this will need to reinit after ajax */
            var self = this;

            /* Single portfolio Slider - single-portfolio.html */
            var singlePortfolioSlider = $('.single-portfolio-slider.owl-carousel');
            if (self.checkSupport(singlePortfolioSlider, $.fn.owlCarousel)) {
                singlePortfolioSlider.owlCarousel({
                    singleItem: true,
                    slideSpeed: 400,
                    autoPlay: 8800,
                    stopOnHover: true,
                    navigation: true,
                    navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
                    pagination: true,
                    responsive: true,
                    mouseDrag: true,
                    autoHeight: false,
                    transitionStyle: "goDown",
                    afterAction: syncPosition,
                    responsiveRefreshRate: 100
                });
            }

            /* Related portfolio - single-portfolio.html */
            var sliderSyncCarousel = $('.slider-thumb-nav.owl-carousel');
            if (self.checkSupport(sliderSyncCarousel, $.fn.owlCarousel)) {
                sliderSyncCarousel.owlCarousel({
                    items: 4,
                    itemsDesktop: [1199, 4],
                    itemsDesktopSmall: [979, 4],
                    itemsTablet: [768, 3],
                    itemsMobile: [479, 2],
                    slideSpeed: 400,
                    autoPlay: 8800,
                    stopOnHover: true,
                    navigation: false,
                    pagination: false,
                    responsive: true,
                    mouseDrag: true,
                    autoHeight: false,
                    responsiveRefreshRate: 100,
                    afterInit: function (el) {
                        el.find(".owl-item").eq(0).addClass("active");
                    }
                });
            }

            if (!singlePortfolioSlider.length && !sliderSyncCarousel.length) {
                return;
            }


            /* Sync Carousels for single-portfolio page */
            function syncPosition(el) {
                var current = this.currentItem;

                $('.slider-thumb-nav.owl-carousel')
					.find(".owl-item")
					.removeClass("active")
					.eq(current)
					.addClass("active");

                if ($('.slider-thumb-nav.owl-carousel').data("owlCarousel") !== undefined) {
                    center(current);
                }
            }

            $('.slider-thumb-nav.owl-carousel').on("click", ".owl-item", function (e) {
                e.preventDefault();
                var number = $(this).data("owlItem");
                singlePortfolioSlider.trigger("owl.goTo", number);
            });

            function center(number) {
                var sync2visible = sliderSyncCarousel.data("owlCarousel").owl.visibleItems,
					num = number,
					found = false,
					i;

                for (i in sync2visible) {
                    if (num === sync2visible[i]) {
                        found = true;
                    }
                }

                if (found === false) {
                    if (num > sync2visible[sync2visible.length - 1]) {
                        sliderSyncCarousel.trigger("owl.goTo", num - sync2visible.length + 2)
                    } else {
                        if (num - 1 === -1) {
                            num = 0;
                        }
                        sliderSyncCarousel.trigger("owl.goTo", num);
                    }
                } else if (num === sync2visible[sync2visible.length - 1]) {
                    sliderSyncCarousel.trigger("owl.goTo", sync2visible[1])
                } else if (num === sync2visible[0]) {
                    sliderSyncCarousel.trigger("owl.goTo", num - 1)
                }

            }
        },
        scrollTopBtnAppear: function () {
            // Show/Hide scrolltop button while scrolling
            var windowTop = $(window).scrollTop(),
		            scrollTop = $('#scroll-top');

            if (windowTop >= 200) {
                scrollTop.addClass('fixed');
            } else {
                scrollTop.removeClass('fixed');
            }

        },
        scrollToAnimation: function (speed, offset, e) {
            /* General scroll to function */
            var targetEl = $(this).attr('href'),
				toTop = false;

            if (!$(targetEl).length) {
                if (targetEl === '#home' || targetEl === '#top') {
                    targetPos = 0;
                    toTop = true;
                } else {
                    return;
                }
            } else {
                var elem = $(targetEl),
					targetPos = offset ? (elem.offset().top + offset) : elem.offset().top;
            }

            if (targetEl || toTop) {
                $('html, body').animate({
                    'scrollTop': targetPos
                }, speed || 1200);
                e.preventDefault();
            }
        },
        menuScrollToAnimation: function () {
            var self = this;
            // Scroll animation to sections when menu links are clicked
            $('#main-menu').find('a').on('click', function (e) {
                self.scrollToAnimation.call(this, 1000, 0, e);

                $(this)
					.closest('li')
					.addClass('active')
					.siblings()
					.removeClass('active');

            });
        },
        scrollToTopAnimation: function () {
            var self = this;
            // Scroll to top animation when the scroll-top button is clicked
            $('#scroll-top').on('click', function (e) {
                self.scrollToAnimation.call(this, 1200, 0, e);
            });
        },
        scrollToClass: function () {
            var self = this;
            // Scroll to animation - predefined class
            // Just add this to any class and add href attribute to target id
            $('.scrollto').on('click', function (e) {
                self.scrollToAnimation.call(this, 1200, 0, e);
            });
        },
        selectBox: function () {
            // Custom select box via selectbox plugin
            // Be sure to include jquery.selectbox.min.js file
            if ($.fn.selectbox) {
                $('.selectbox').selectbox({
                    effect: "fade"
                });
            }

        },
        bootstrapSwitch: function () {
            //Bootstrap switch
            if ($.fn.bootstrapSwitch) {
                $('.switch').bootstrapSwitch();
            }
        },
        tooltip: function () {
            // Bootstrap tooltip
            if ($.fn.tooltip) {
                $('.add-tooltip').tooltip();
            }
        },
        popover: function () {
            // Bootstrap popover
            if ($.fn.popover) {
                $('.add-popover').popover({
                    trigger: 'focus'
                });
            }
        },
        countTo: function () {
            // CountTo plugin used count animations for homepages
            if ($.fn.countTo) {
                if ($.fn.waypoint) {
                    $('.count').waypoint(function () {
                        $(this).countTo();
                    }, {
                        offset: function () {
                            return ($(window).height() - 100);
                        },
                        triggerOnce: true
                    });
                } else {
                    $('.count').countTo();
                }
            } else {
                // fallback if count plugin doesn't included
                // Get the data-to value and add it to element
                $('.count').each(function () {
                    var $this = $(this),
						countValue = $this.data('to');

                    $this.text(countValue);
                });
            }
        },
        progressBars: function () {
            var self = this;
            // Calculate and Animate Progress 
            // With waypoing plugin calculate width of the progress bar
            if ($.fn.waypoint) {
                $('.progress-animate').waypoint(function () {
                    if (!$(this).hasClass('circle-progress')) {
                        var $this = $(this),
						progressVal = $(this).data('width'),
						progressText = $this.find('.progress-text');

                        $this.css({ 'width': progressVal + '%' }, 400);
                        progressText.fadeIn(500, function () {
                            $(this).removeClass('progress-animate');
                        });
                    } else {
                        // Animate knob --- Circle progrss bars
                        self.animateKnob();
                    }
                }, {
                    offset: function () {
                        return ($(window).height() - 10);
                    }
                });


            } else {
                // Fallback if the waypoint plugin isn't inclueded
                // Get the value and calculate width of progress bar
                $('.progress-animate').each(function () {
                    var $this = $(this),
						progressVal = $(this).data('width'),
						progressText = $this.find('.progress-text');

                    $this.css({ 'width': progressVal + '%' }, 400);
                    progressText.fadeIn(500);
                });

            }
        },
        registerKnob: function () {
            // Register knob plugin
            if ($.fn.knob) {
                $('.knob').knob({
                    bgColor: '#fff'
                });
            }
        },
        animateKnob: function () {
            // Animate knob
            if ($.fn.knob) {
                $('.knob').each(function () {
                    var $this = $(this),
						container = $this.closest('.progress-animate'),
						animateTo = $this.data('animateto'),
						animateSpeed = $this.data('animatespeed')
                    $this.animate(
			                { value: animateTo },
			                {
			                    duration: animateSpeed,
			                    easing: 'swing',
			                    progress: function () {
			                        $this.val(Math.round(this.value)).trigger('change');
			                    },
			                    complete: function () {
			                        container.removeClass('progress-animate');
			                    }
			                });

                });
            }
        },
        scrollAnimations: function () {
            /* 	// Wowy Plugin
				Add Html elements wow and animation class 
				And you can add duration via data attributes
				data-wow-duration: Change the animation duration
				data-wow-delay: Delay before the animation starts
				data-wow-offset: Distance to start the animation (related to the browser bottom)
				data-wow-iteration: Number of times the animation is repeated
			*/

            // Check for class WOW // You need to call wow.min.js and animate.css for scroll animations to work
            if (typeof WOW === 'function') {
                new WOW({
                    boxClass: 'wow',      // default
                    animateClass: 'animated', // default
                    offset: 0          // default
                }).init();
            }

        },
        prettyPhoto: function () {
            // Portfolio prettPhoto Plugin - Lightbox for gallery pages etc..
            if ($.fn.prettyPhoto) {

                $("a[data-rel^='prettyPhoto']").prettyPhoto({
                    hook: 'data-rel',
                    animation_speed: 'fast',
                    slideshow: 6000,
                    autoplay_slideshow: true,
                    show_title: false,
                    deeplinking: false,
                    social_tools: '',
                    overlay_gallery: true
                });
            }

        },
        flickerFeed: function () {
            // Flickr feed plugin - Footer and Sidebar 
            if ($.fn.jflickrfeed) {

                /* Sidebar */
                $('ul.sidebar-flickr-widget').jflickrfeed({
                    limit: 8,
                    qstrings: {
                        id: '52617155@N08'
                    },
                    itemTemplate: '<li>' + '<a href="{{image}}" title="{{title}}">' + '<img src="{{image_s}}" alt="{{title}}" />' + '</a>' + '</li>'
                });

            }
        },
        contactForm: function () {
            // Contact form basic valitation with validate jquery plugin
            // Ajax php files hasn't included so it is not functional yet
            // it will be added if there are requests for it
            if ($.fn.validate) {
                $('#contact-form').validate({
                    rules: {
                        contactname: 'required',
                        contactemail: {
                            required: true,
                            email: true
                        },
                        contactmessage: {
                            required: true,
                            minlength: 50
                        }
                    },
                    messages: {
                        contactname: "This field is required. Please enter your name.",
                        contactemail: {
                            required: "This field is required. Please enter your email address.",
                            email: "Please enter a valid email address."
                        },
                        contactmessage: {
                            required: "This field is required. Please enter your message.",
                            minlength: "Your message must be at least 50 characters long."
                        }
                    },
                    submitHandler: function (form) {

                        /* ajax request will be updated here */
                        $.ajax({
                            type: 'post',
                            url: 'php/mail.php',
                            data: $(form).serialize(),
                        }).done(function (data) {
                            if (data == 'success') {
                                alert('Email has been sent successfully!')
                            } else if (data == 'already') {
                                alert('You already sent a message. Please try again later');
                            } else {
                                alert('There is an error please try again later!');
                            }
                        }).error(function () {
                            alert('There is an error please try again later!');
                        });

                        return false;
                    }
                });
            }
        },
        contactFormFixes: function () {
            /* Contact form label animation check*/
            /* if contact form input values are not empty, do not back move  its label*/
            var contactFrom = $('#contact-form');
            contactFrom.find('input, textarea').on('blur', function () {
                var $this = $(this),
					inputVal = $this.val(),
					animatedLabel = $this.siblings('.animated-label');

                if (inputVal !== '') {
                    animatedLabel.addClass('not-empty');
                } else {
                    animatedLabel.removeClass('not-empty');
                }
            });

            /* Fix for reset button Remove the not-empty class when reset button is clicked */
            contactFrom.find('input[type="reset"]').on('click', function () {
                contactFrom.find('.animated-label').removeClass('not-empty');
            });


        },
        scrollSpyRefresh: function () {
            /* When using scrollspy in conjunction with adding or removing of elements 
			from the Dom, we need to use this function to refresh scrollspy like so: */
            $('[data-spy="scroll"]').each(function () {
                var $spy = $(this).scrollspy('refresh')
            });
        },
        parallax: function () {
            // Parallax - if not mobile  with stellar js plugin 
            if (!Geass.mobile && $.fn.stellar) {
                $(window).stellar({
                    verticalOffset: 0,
                    horizontalOffset: 0,
                    horizontalScrolling: false
                });
            }
        },
        // Portfolio/Gallery pages isotope
        calculateWidth: function () {
            // Calculate portfolio items width for better responsive items
            var widthPort = $(window).width(),
                    contWidth = this.container.width(),
                    maxColumn = this.container.data('maxcolumn'),
                    itemW;

            if (widthPort > 1170) {
                itemW = (maxColumn) ? maxColumn : 5;
            } else if (widthPort > 960) {
                itemW = (maxColumn) ? ((maxColumn > 4) ? 4 : 3) : 4;
            } else if (widthPort > 767) {
                itemW = 3;
            } else if (widthPort > 540) {
                itemW = 2;
            } else {
                itemW = 1;
            }

            var targetItems = this.container.find('.portfolio-item');
            if (itemW >= 4 && targetItems.hasClass('wide')) {
                this.container.find('.wide').css('width', (Math.floor(contWidth / itemW) * 2));
                targetItems.not('.wide').css('width', Math.floor(contWidth / itemW));
            } else {
                targetItems.css('width', Math.floor(contWidth / itemW));
            }

        },
        isotopeActivate: function () {
            // Trigger for isotop plugin
            if ($.fn.isotope) {
                var container = this.container,
					layoutMode = container.data('layoutmode');

                var iso = container.isotope({
                    itemSelector: '.portfolio-item',
                    layoutMode: (layoutMode) ? layoutMode : 'masonry',
                    transitionDuration: 0
                }).data('isotope');
            }

            // checked layout mode via instance
            // console.log(iso);
        },
        isotopeReinit: function () {
            // Recall for isotope plugin
            if ($.fn.isotope) {
                this.container.isotope('destroy');
                this.isotopeActivate();
            }
        },
        isotopeFilter: function () {
            // Isotope plugin filter handle
            // Isotope plugin filter handle
            var self = this,
				filterContainer = $('#portfolio-filter');

            filterContainer.find('a').on('click', function (e) {
                var $this = $(this),
					selector = $this.attr('data-filter'),
					animationclass = self.container.data('animationclass'),
					customAnimation = (animationclass) ? animationclass : 'fadeInUp';

                filterContainer.find('.active').removeClass('active');

                // Delete css Animation and class 
                // They effects filtering
                self.container.find('.portfolio-item').removeClass('animate-item ' + customAnimation);

                // And filter now
                self.container.isotope({
                    filter: selector,
                    transitionDuration: '0.8s'
                });

                $this.addClass('active');
                e.preventDefault();
            });
        },
        elementsAnimate: function () {
            // Appear animation on load for gallery/portfolio items
            var animationclass = this.container.data('animationclass'),
				customAnimation = (animationclass) ? animationclass : 'fadeInUp',
				elemLen = this.container.find('.animate-item').length,
				count = 0; // to keep element count


            this.container.find('.animate-item').each(function () {
                var $this = $(this),
                    time = $this.data('animate-time');

                ++count;

                setTimeout(function () {
                    $this.addClass('animated ' + customAnimation);
                }, time);

                if (count === elemLen) {
                    this.portfolioItemsAnimated = true;
                }
            });

            /* relayout isotope after animation */
            if ($.fn.isotope && this.portfolioItemsAnimated) {
                this.container.isotope('layout');
            }
        },
        scrollTriggerforPortfolioAnim: function () {
            if ($.fn.waypoint) {
                /* Trigger Portfolio item Animations */
                this.container.waypoint(
					Geass.elementsAnimate.bind(this),
					{
					    offset: '90%',
					    triggerOnce: true
					}
				);
            } else {
                Geass.elementsAnimate();
            }
        },
        hoverAnimation: function () {
            // Hover animation for gallery/portfolio pages
            var rotate3d = this.container.data('rotateanimation'),
				rotate3dActive = (rotate3d) ? rotate3d : false;

            if ($.fn.hoverdir) {
                this.container.find('li').each(function () {
                    $(this).hoverdir({
                        speed: 400,
                        hoverDelay: 0,
                        hoverElem: '.portfolio-overlay',
                        rotate3d: rotate3dActive
                    });
                });
            }
        },
        openProject: function () {
            // Open portfolio project with custom animations
            var self = this,
				targetEl = $('#portfolio-single-content'),
				targetElIn = targetEl.find('.single-portfolio');


            $('.open-btn').on('click', function (e) {
                e.preventDefault();
                var $this = $(this),
					parentEl = $this.closest('.portfolio-item');


                if (!targetEl.is(':hidden')) {

                    self.container.find('.portfolio-item.active').removeClass('active');

                    self.loadProject.call($this, targetEl, parentEl);

                } else {

                    self.loadProject.call(this, targetEl, parentEl);

                }
            });
        },
        loadProject: function (targetEl, parentEl) {
            var $this = $(this),
				targetPro = $this.attr('href');

            if (targetPro === '') {
                alert('Path is empyt! Please use right path for the project!');
                return;
            }

            if (targetPro.indexOf('.html') == -1) {
                alert('Not a valid path! Please use right path for the project!');
                return;
            }

            var ajaxCall = new $.Deferred();

            $.when(ajaxCall).done(function (data) {

                targetEl.animate({ 'height': 'show' }, 600, function () {

                    var targetPosition = ((targetEl.offset().top) - 110);
                    $('html, body').animate({ 'scrollTop': targetPosition }, 700);

                    // Refresh scrollspyt
                    $('[data-spy="scroll"]').each(function () {
                        var $spy = $(this).scrollspy('refresh')
                    });
                    parentEl.addClass('active');
                });

                Geass.closeProject();
                Geass.singlePortfolioOwl();
            });

            targetEl.load(targetPro + ' #project-content', function (response, status, xhr) {
                ajaxCall.resolve();
            });

        },
        closeProject: function () {
            // Close Projects
            var self = this,
				targetEl = $('#portfolio-single-content'),
				targetElIn = targetEl.find('.single-portfolio');

            $('.portfolio-close').on('click', function (e) {

                $(targetEl).animate({ 'height': 'hide' }, 400, function () {
                    self.container.find('.portfolio-item.active').removeClass('active');
                    $(this).html('')
                });

                e.preventDefault();
            });

        }
    };

    Geass.init();

    /* On load */
    $(window).on('load', function () {
    });

    // Scroll Event
    $(window).on('scroll', function () {
        /* Display Scrol to Top Button */
        Geass.scrollTopBtnAppear();

    });

    // Resize Event 
    // Smart resize if plugin not found window resize event
    if ($.event.special.debouncedresize) {
        $(window).on('debouncedresize', function () {

            /* Fix Home section height for boxed version */
            Geass.homeSectionHeight();

            /* Portfolio items / isotope retrigger */
            Geass.calculateWidth();
            Geass.isotopeReinit();

            /* Refresh scrollspy */
            Geass.scrollSpyRefresh();

        });
    } else {
        $(window).on('resize', function () {

            /* Fix Home section height for boxed version */
            Geass.homeSectionHeight();

            /* Portfolio items / isotope retrigger */
            Geass.calculateWidth();
            Geass.isotopeReinit();

            /* Refresh scrollspy */
            Geass.scrollSpyRefresh();
        });
    }


})(jQuery);