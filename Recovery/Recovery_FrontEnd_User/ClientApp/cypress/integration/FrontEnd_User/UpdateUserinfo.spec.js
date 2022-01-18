/* ==== Test Created with Cypress Studio ==== */
it('UpdateUserInfo', function() {
  /* ==== Generated with Cypress Studio ==== */
  cy.visit('http://localhost:3000/');
  cy.get('#email').clear();
  cy.get('#email').type('t@t');
  cy.get('#password').clear();
  cy.get('#password').type('t');
  cy.get('.formFieldButton').click();
  cy.get('#email').clear();
  cy.get('#email').type('t@t');
  cy.get('#password').clear();
  cy.get('#password').type('t');
  cy.get('.formFieldButton').click();
  cy.get(':nth-child(3) > .form-control').clear();
  cy.get(':nth-child(3) > .form-control').type('mota');
  cy.get('.btn').click();
  cy.get(':nth-child(5) > .text-light').click();
  /* ==== End Cypress Studio ==== */
});