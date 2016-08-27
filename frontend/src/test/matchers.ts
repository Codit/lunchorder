beforeEach(() => {
  jasmine.addMatchers({

    toHaveText: function() {
      return {
        compare: function(actual : any, expectedText : any) {
          var actualText = actual.textContent;
          return {
            pass: actualText == expectedText,
            get message() { return 'Expected ' + actualText + ' to equal ' + expectedText; }
          };
        }
      };
    },

    toContainText: function() {
      return {
        compare: function(actual : any, expectedText : any) {
          var actualText = actual.textContent;
          return {
            pass: actualText.indexOf(expectedText) > -1,
            get message() { return 'Expected ' + actualText + ' to contain ' + expectedText; }
          };
        }
      };
    }
  });
});
