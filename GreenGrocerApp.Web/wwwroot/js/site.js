// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Enhancements: carousel support, smooth scrolling and a back to top button.

/* global $ */

$(function () {
    // Smooth scrolling for on-page anchors
    $('a[href^="#"]').on('click', function (e) {
        var target = $(this.getAttribute('href'));
        if (target.length) {
            e.preventDefault();
            $('html, body').animate({ scrollTop: target.offset().top }, 600);
        }
    });

    // Back to top button
    var backToTop = $('<button />', {
        id: 'backToTop',
        text: '↑',
        class: 'btn btn-primary',
        title: 'Back to top'
    }).css({
        position: 'fixed',
        bottom: '20px',
        right: '20px',
        display: 'none',
        'z-index': 1000
    });

    $('body').append(backToTop);

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 200) {
            backToTop.fadeIn();
        } else {
            backToTop.fadeOut();
        }
    });

    backToTop.on('click', function () {
        $('html, body').animate({ scrollTop: 0 }, 600);
    });

    // Initialize Bootstrap carousel if present
    $('.carousel').carousel();
});