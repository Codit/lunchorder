// import {
//   inject,
//   async,
//   addProviders,
//   fakeAsync,
//   tick,
//   ComponentFixture,
//   TestComponentBuilder
// } from '@angular/core/testing';
// import { provide } from '@angular/core';
// import { ConfigService } from '../app/services/configService';
// import { BadgeComponent } from '../app/app.badge';

// class MockLoginService extends LoginService {
//   login(pin: number) {
//     return Promise.resolve(true);
//   }
// }

// describe('greeting component', () => {
//   var builder;

//   beforeEach(() => {
//     addProviders([
//       provide(LoginService, { useClass: MockLoginService }),
//       ConfigService
//     ]);
//   });

//   beforeEach(inject([TestComponentBuilder], (tcb : any) => {
//     builder = tcb;
//   }));

//   it('should ask for PIN', async(() => {
//     builder.createAsync(BadgeComponent).then((fixture: ComponentFixture<BadgeComponent>) => {
//       fixture.detectChanges();
//       var compiled = fixture.debugElement.nativeElement;

//       expect(compiled).toContainText('Enter PIN');
//       expect(compiled.querySelector('h3')).toHaveText('Status: Enter PIN');
//     });
//   }));

//   it('should change greeting', async(() => {
//     builder.createAsync(GreetingComponent).then((fixture: ComponentFixture<GreetingComponent>) => {
//       fixture.detectChanges();

//       fixture.debugElement.componentInstance.greeting = 'Foobar';

//       fixture.detectChanges();
//       var compiled = fixture.debugElement.nativeElement;
//       expect(compiled.querySelector('h3')).toHaveText('Status: Foobar');
//     });
//   }));

//   it('should override the template', async(() => {
//     builder.overrideTemplate(GreetingComponent, `<span>{{greeting}}<span>`)
//       .createAsync(GreetingComponent).then((fixture: ComponentFixture<GreetingComponent>) => {
//           fixture.detectChanges();

//           var compiled = fixture.debugElement.nativeElement;
//           expect(compiled).toHaveText('Enter PIN');
//         });
//       }));

//   it('should accept pin', async(() => {
//     builder.createAsync(GreetingComponent).then((fixture: ComponentFixture<GreetingComponent>) => {
//       fixture.detectChanges();
//       var compiled = fixture.debugElement.nativeElement;
//       compiled.querySelector('button').click();

//       fixture.debugElement.componentInstance.pending.then(() => {
//         fixture.detectChanges();
//         expect(compiled.querySelector('h3')).toHaveText('Status: Welcome!');
//       });
//     });
//   }));

//   it('should accept pin (with fakeAsync)', fakeAsync(() => {
//     var fixture;
//     builder.createAsync(GreetingComponent).then((rootFixture) => {
//       fixture = rootFixture });
//     tick();

//     var compiled = fixture.debugElement.nativeElement;
//     compiled.querySelector('button').click();

//     tick();
//     fixture.detectChanges();
//     expect(compiled.querySelector('h3')).toHaveText('Status: Welcome!');
//   }));
// });
