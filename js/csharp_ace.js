window.onload = start_ace;
function start_ace() {
    var editor = ace.edit("editor");
    editor.setTheme("ace/theme/cobalt");
    var LangMode = require("ace/mode/csharp").Mode;
    editor.getSession().setMode(new LangMode());
    var textarea = $('textarea[name="input_area"]').hide();
    editor.getSession().setValue(textarea.val());
    editor.getSession().on('change', function () {
        textarea.val(editor.getSession().getValue());
    });
    editor.resize();
}