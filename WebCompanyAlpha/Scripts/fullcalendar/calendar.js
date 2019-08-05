var calendar = $('#calendar');
function show_messages() {
    calendar.fullCalendar('refetchEvents');
}
$(document).ready(function () {
    show_messages();
    setInterval('show_messages()', 10000);
    $('#calendar').fullCalendar({
        firstday: 1,
        header:
        {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'οюнь', 'οюль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв.', 'Фев.', 'Март', 'Апр.', 'Май', 'οюнь', 'οюль', 'Авг.', 'Сент.', 'Окт.', 'Ноя.', 'Дек.'],
        dayNames: ["Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"],
        dayNamesShort: ["ВС", "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ"],
        buttonText: {
            prev: "<--",
            next: "-->",
            prevYear: "<<-",
            nextYear: "->>",
            today: "Сегодня",
            month: "Месяц",
            week: "Неделя",
            day: "День"
        },
        locale: 'ru',
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/OrderRoom/GetOrderRoomJSonList/',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                id: data.ID,
                                title: data.Name,
                                description: data.ID,
                                start: data.Start,
                                end: data.End,
                                resource: data.ID,
                                event_id: data.ID,
                                backgroundColor: data.BackgroundColor,
                                borderColor: data.BorderColor
                            }, true);
                    });

                    callback(events);
                }
            });
        },
        //eventSources: "/Home/GetJSon/"
        /*events:[
            { // this object will be "parsed" into an Event Object
                title: 'The Title', // a property!
                start: '2019-08-01 14:00:00', // a property!
                end: '2019-08-02 15:00:00' // a property! ** see important note below about 'end' **
                
            }
            ]*/
        eventClick: function (calEvent, jsEvent, view) {
            window.location.href = "/OrderRoom/Details/" + calEvent.description;
        },

        selectable: true,
        select: function (start, end) {
            window.location.href = "/OrderRoom/Insert?start=" + start.toString("dd.MM.yyyy HH:mm:ss");
        },

        editable: false
    });
});
