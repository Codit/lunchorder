import {Directive, ElementRef, Input, HostListener} from '@angular/core';
import {Observable} from 'rxjs/rx';

import {WindowRef} from '../services/windowService';

@Directive({
    selector: '[stick-rx]'
})
export class StickRxDirective {

    constructor(private _element: ElementRef, private _window: WindowRef) {

        this.subscribeForScrollEvent();
    }

    stickyOffset: number; 
    subscribeForScrollEvent() {
        console.log("subscribeForScrollEvent");
        var obs = Observable.fromEvent(window, 'scroll');
        this.stickyOffset = this._element.nativeElement.offsetTop;

        obs.subscribe((e) => this.handleScrollEvent(e));
    }

    handleScrollEvent(e: any) {
        console.log("handle scroll event: " + e);


                // var stickyOffset = $('.sticky').offset().top;

        var scroll = this._window.nativeWindow.pageYOffset;

        if (scroll >= this.stickyOffset) { this._element.nativeElement.classList.add('stick-rx'); }

        else { this._element.nativeElement.classList.remove('stick-rx'); }

        // if (this._window.nativeWindow.pageYOffset > 100) {
        //     this._element.nativeElement.classList.add('stick-rx');
        // } else {
        //     this._element.nativeElement.classList.remove('stick-rx');
        // }
    }
}