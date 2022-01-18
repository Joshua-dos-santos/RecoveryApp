// UpdateUserInjury.spec.js created with Cypress
//
// Start writing your Cypress tests below!
// If you're unfamiliar with how Cypress works,
// check out the link below and learn how to write your first test:
// https://on.cypress.io/writing-first-test
/* ==== Test Created with Cypress Studio ==== */
it('UpdateInjury', function() {
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
  cy.get('.navbar-nav > :nth-child(1) > .text-light').click();
  cy.get('#part_of_Body').select('upper leg');
  cy.get('#description').clear();
  cy.get('#description').type('test');
  cy.get('.rangeslider__handle').click();
  cy.get('.formFieldCheckbox').check();
  cy.get('.formFieldButton').click();
  cy.get(':nth-child(5) > .text-light').click();
  /* ==== End Cypress Studio ==== */
});
