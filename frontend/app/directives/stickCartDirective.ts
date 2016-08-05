import {Directive, ElementRef, Input, HostListener} from '@angular/core';
import {Observable} from 'rxjs/Rx';

import {WindowRef} from '../services/windowService';

@Directive({
    selector: '[stick-cart-rx]'
})
export class StickCartDirective {

    constructor(private _element: ElementRef, private _window: WindowRef) {

        this.subscribeForScrollEvent();
    }

    isSet: boolean;
    stickyOffset: number; 
    subscribeForScrollEvent() {
        var obs = Observable.fromEvent(window, 'scroll');
        this.stickyOffset = this._element.nativeElement.offsetTop - 125;

        obs.subscribe((e : any) => this.handleScrollEvent(e));
    }

    handleScrollEvent(e: any) {
        var scroll = this._window.nativeWindow.pageYOffset;
        var rowHeight = this._element.nativeElement.parentElement.clientHeight -250;
        var margin = this.stickyOffset - scroll;

        if (scroll >= this.stickyOffset && Math.abs(margin) < rowHeight) { 
            this.isSet = false;
            this._element.nativeElement.classList.add('fixed');
            // todo replace with original top from subscribe
            this._element.nativeElement.style["marginTop"] = "80px";
     }

        else { this._element.nativeElement.classList.remove('fixed');
                
                if (margin < 0 && !this.isSet) {
                    this.isSet = true;
                    this._element.nativeElement.style["marginTop"] = Math.abs(margin) + "px";
                }
        }
    }
}