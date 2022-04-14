redirectToCheckout = function (sessionId) {
  var stripe = Stripe("pk_test_51K9pTpSJ46N7ItBNBPcs3g7Ohr5UGKn0TdghAijrCZyShQNoUAmKnFfJgfOpZ2kGsvi1OXN5j2bUSzNihfi34KWd00gfiHqtnb");
  stripe.redirectToCheckout({ sessionId: sessionId });
}