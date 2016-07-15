import {
  describe,
  expect,
  beforeEach,
  it,
  beforeEachProviders,

  async,
  inject,
  TestComponentBuilder,
  ComponentFixture
} from '@angular/core/testing';
import { Component } from '@angular/core';
import { AppComponent } from '../app/app.component';
import { AdalService } from 'angular2-adal/core';
import { MenuService } from '../app/services/menuService';

beforeEachProviders(() => {
  return [
      HTTP_PROVIDERS,
      provide(XHRBackend, {useClass: MockBackend}),
      BlogService
    ];
});


it('should get blogs', 
  inject([XHRBackend, MenuService], (mockBackend, blogService) => {
    // first we register a mock response - when a connection 
    // comes in, we will respond by giving it an array of (one)
    // blog entries
    mockBackend.connections.subscribe(
      (connection: MockConnection) => {
        connection.mockRespond(new Response(
          new ResponseOptions({
              body: [
                {
                  id: 26,
                  contentRendered: "<p><b>Hi there</b></p>",
                  contentMarkdown: "*Hi there*"
                }]
            }
          )));
      });
    // with our mock response configured, we now can 
    // ask the blog service to get our blog entries
    // and then test them
    blogService.getBlogs().subscribe((blogs: BlogEntry[]) => {
      expect(blogs.length).toBe(1);
      expect(blogs[0].id).toBe(26);
    });

  }));