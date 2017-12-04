var val = {
    01: { id: 01, text: 'Isis' },
    02: { id: 02, text: 'Sophia' },
    03: { id: 03, text: 'Alice' },
    04: { id: 04, text: 'Isabella' },
    05: { id: 05, text: 'Manuela' },
    06: { id: 06, text: 'Laura' },
    07: { id: 07, text: 'Luiza' },
    08: { id: 08, text: 'Valentina' },
    09: { id: 09, text: 'Giovanna' },
    10: { id: 10, text: 'Maria Eduarda' },
    11: { id: 11, text: 'Helena' },
    12: { id: 12, text: 'Beatriz' },
    13: { id: 13, text: 'Maria Luiza' },
    14: { id: 14, text: 'Lara' },
    15: { id: 15, text: 'Julia' }
};

var pick = $("#pickList").pickList({ data: val });
$("#getSelected").click(function () {
    console.log(pick.getValues());
});
