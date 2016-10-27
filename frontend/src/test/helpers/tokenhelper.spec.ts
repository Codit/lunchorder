// import { inject, async, addProviders, fakeAsync, tick } from '@angular/core/testing';
// import { TokenHelper } from '../../app/helpers/tokenHelper';
// import { ConfigService } from '../../app/services/configService'

// describe('token helper', () => {
//   beforeEach(() => {
//     addProviders([TokenHelper, ConfigService]);
//   });

//   it('should parse the token', inject([TokenHelper], (tokenHelper : TokenHelper) => {
//     var idToken = 'eyJ0eXAiOiJKV1QiLCJ9.eyJhdWQiOiI2NAif.t_a6R-PMxGoL4Ky_lAmjrgw&state=4854bff1-6ed4-47f8-87f8-8b98c4382178&session_state=854c31c9-e04a-462f-b1a3-bdf78e63a4a0';
//     var token = 'eyJ0eXAiOiJKV1QiLCJ9.eyJhdWQiOiI2NAif.t_a6R-PMxGoL4Ky_lAmjrgw';

//     var currentUrl = tokenHelper.getCurrentURL();
//     spyOn(tokenHelper, 'getCurrentURL').and.returnValue(`http://www.lunchorder.be/#id_token=${idToken}`);
    
//     var actualToken = tokenHelper.getToken();
//     expect(actualToken).toBe(token);
//   }));
// });