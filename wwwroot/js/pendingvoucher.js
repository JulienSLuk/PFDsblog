$(document).ready(function () {
    $("tbody").on("click", ".vitem", function (e) {
        e.preventDefault();
        let issuingid = $(this).data("issuingid");
        let month = $(this).data("month");
        let year = $(this).data("year");
        let amount = $(this).data("amount");
        let memberid = $(this).data("memberid");

        let content = `
        <form method="post" action="/SalesPersonnel/UpdateVoucher/${issuingid}">
            <div class="modal-body">
                <p>Issuing ID: ${issuingid}</p>
                <p>Month: ${month}</p>
                <p>Year: ${year}</p>
                <p>Amount: ${amount}</p>
                <input type="hidden" name="memberID" value="${memberid}">
                <input type="text" class="form-control" name="voucherSN" placeholder="Voucher Serial No." required>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <input type="submit" class="btn btn-primary">
            </div>
        </form>
        `;

        document.querySelector(".modal-info").innerHTML = content;

    });

});