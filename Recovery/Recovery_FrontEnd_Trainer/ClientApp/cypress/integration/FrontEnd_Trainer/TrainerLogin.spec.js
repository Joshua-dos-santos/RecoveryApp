/* ==== Test Created with Cypress Studio ==== */
it('LoginTrainer', function() {
  /* ==== Generated with Cypress Studio ==== */
  cy.visit('http://localhost:3001/');
  cy.get('#email').clear();
  cy.get('#email').type('test@test');
  cy.get('#password').clear();
  cy.get('#password').type('test');
  cy.get('.formFieldButton').click();
  cy.get('.nav-item > .text-light').click();
  /* ==== End Cypress Studio ==== */
});