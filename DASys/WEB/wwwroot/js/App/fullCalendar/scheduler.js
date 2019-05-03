(function ($) {
    'use strict';

    $(document).ready(function () {
        $('#m-scheduler-calendar').fullCalendar({
            header: {
                left: 'prev,next,title',
                center: '',
                right: 'basicDay,basicWeek,month',
            },
            navLinks: true,
            hiddenDays: [0],
            editable: true,
            selectable: true,
            selectHelper: true,
            eventLimit: true,
            columnFormat: 'dddd',
            contentHeight: 800,
            events: [
                {
                    title: 'Llamar a Raul',
                    start: moment({ hour: 19, minute: 10 }),
                    end: moment({ hour: 21, minute: 10 }),
                    type: 'info',
                    typeTitle: 'S',
                    desc: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'
                },
                {
                    title: 'Llamar a Jose',
                    start: moment({ hour: 21, minute: 15 }),
                    end: moment({ hour: 22, minute: 15 }),
                    type: 'success',
                    typeTitle: 'P',
                    desc: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'
                },
                {
                    title: 'Email a Mary',
                    start: moment({ hour: 11, minute: 0 }).day(5),
                    end: moment({ hour: 14, minute: 0 }).day(5),
                    type: 'info',
                    typeTitle: 'S',
                    desc: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'
                },
                {
                    title: 'Llamar a Pedro',
                    start: moment({ hour: 15, minute: 0 }).day(5),
                    end: moment({ hour: 16, minute: 0 }).day(5),
                    type: 'success',
                    typeTitle: 'P',
                    desc: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'
                },
                {
                    title: 'Foo event',
                    start: '2018-05-08 22:02:54',
                    end: '',
                    type: 'success',
                    desc: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.'
                }
            ],
            eventRender: function (event, element) {
                var type = $('<span/>').addClass('m-scheduler__event-type m-scheduler__event-type--' + event.type.toLowerCase()).text(event.typeTitle);
                var timeFrom = moment(event.start).format("LT");
                var timeEnd = moment(event.end).format("LT");

                element.find(".fc-time").remove();
                element.find('.fc-content').prepend(type);
                element.popover({
                    html: true,
                    content: '<div class="m-scheduler__event-heading">' + type.get(0).outerHTML +
                        ' <span class="m-scheduler__event-title">' + event.title + '</span></div>' +
                        '<div class="m-scheduler__event-time">' + timeFrom + ' to ' + timeEnd + '</div>' +
                        '<div class="m-scheduler__event-desc">' + event.desc + '</div>',
                    title: moment(event.start).format("D MMMM YYYY"),
                    container: '.m-scheduler'
                });

                element.on('show.bs.popover', function () {
                    $('.popover.show').not(element).popover('hide');
                })
            },
            eventClick: function (event, jsEvent, view) {

            }
        });
        $("#m-scheduler-calendar .fc-header-toolbar .fc-right .fc-button-group").append("<button class='fc-button lu-agenda-new-event' id='NuevoEvento'>Nuevo</button><button class='fc-button lu-agenda-remove-event'>Eliminar</button>");



        //$(document).on('click', function (e) {
        //    if ($('#m-scheduler-calendar').length && !$(e.target).closest('.popover').length && !$(e.target).closest('.fc-day-grid-event').length) {
        //        $('.popover.show').popover('hide');
        //    }
        //})
    });



})(jQuery);
