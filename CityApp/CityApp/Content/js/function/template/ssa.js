const setupSSA = (selector, ssaURL, ssaPlaceHolder, templateResultCallBack, templateSelectionCallBack) => {
    $(selector).select2({
        ajax: {
            url: ssaURL,
            dataType: "json",
            delay: 50,
            data: (params) => {
                return {
                    q: params.term, // search term
                    page: params.page
                };
            }
        },
        placeholder: ssaPlaceHolder,
        escapeMarkup: (markup) => markup, // let our custom formatter work
        minimumInputLength: 1,
        templateResult: templateResultCallBack,
        templateSelection: templateSelectionCallBack
    });
};


//*** Customer SSA Template ***//
const customerTemplateResult = (data) => {
    if (data.loading)
        return data.text;
    let markup = `<div class="row">
                        <div class="col-4"> ${data.customerName} </div>
                        <div class="col-4"> ${data.lastName}  </div>
                        <div class="col-4"> ${data.customerCode}  </div>
                    </div>`;
    return markup;
};

const customerTemplateSelection = (data) => {
    if (data.text !== "") return data.text;
    if (data.id === "") return 'Customer';
    return data.customerName + " " + data.customerCode;
};
//*** end Customer SSA Template ***//




//*** Driver SSA Template ***//
const driverTemplateResult = (data) => {
    if (data.loading)
        return data.text;
    let markup = `<div class="row">
                        <div class="col"> ${data.driverName} </div>
                        <div class="col"> ${data.driverCode}  </div>
                    </div>`;
    return markup;
};

const driverTemplateSelection = (data) => {
    if (data.text !== "") return data.text;
    if (data.id === "") return 'Driver';
    return data.driverName;
};
//*** end Driver SSA Template ***//