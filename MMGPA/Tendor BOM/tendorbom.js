
$(document).ready(function () {

    // administrative number & title
    $('#AdminApproveNo').select2({
        theme: 'classic',
        placeholder: 'Select here.....',
        allowClear: false,
    });

    

    // sumo select
    $('#xxxxxx').on('sumo:opening', function () {
        $('.select-all').css('height', '40px');
    });

    // sumo select elements
    $('#xxxxxx').SumoSelect({
        search: true,
        searchText: "search here....",
        multiSelect: true,
        okCancelInMulti: true,
        prefix: "",
        up: false, // list at top side
        selectAll: true, // not working properly
        clearAll: true, // not working at all
        renderOption: function (data, escape) {
            // Check if the option is the "Select Values" item
            if (data.value === "0") {
                // Add a class to "Select Values" item to handle differently
                return '<div class="title special-item">' + escape(data.text) + '</div>';
            } else {
                return '<div><label><input type="checkbox" />' + escape(data.text) + '</label></div>';
            }
        }
    });

    // Add an event listener to handle the specific item
    $('#xxxxxx').on('sumo:opened', function () {
        // Check if the "Select Values" item is present
        if ($('#xxxxxx .special-item').length > 0) {
            // Disable multiselect for the "Select Values" item
            $('#xxxxxx .special-item input').prop('disabled', true);
        }
    });






    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

            

        }, 0);
    });

});