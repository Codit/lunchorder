import { Directive, ElementRef, Input, HostListener } from '@angular/core';
import { Observable } from 'rxjs';
import { WindowRef } from '../services/windowRef';

@Directive({
    selector: '[stick-cart-rx]'
})
export class StickCartDirective {

    constructor(private _element: ElementRef, private windowRef: WindowRef) {

        this.subscribeForScrollEvent();
    }

    isSet: boolean;
    subscribeForScrollEvent() {
        var obs = Observable.fromEvent(window, 'scroll');

        obs.subscribe((e: any) => this.handleScrollEvent(e));
    }

    handleScrollEvent(e: any) {
        var parentElement = this._element.nativeElement.parentElement;
        // the distance of the current element relative to the top of the offsetParent node.
        var elementScrollHeight = parentElement.offsetTop;

        // scrolled from the upper left corner of the window, vertically
        var userScrolledHeight = this.windowRef.nativeWindow.pageYOffset;

        // the inner height of an element in pixels, including padding but not the horizontal scrollbar height, border, or margin.
        var elementHeight = parentElement.clientHeight;

        var margin = elementScrollHeight - userScrolledHeight;

        var isOverscrolled = userScrolledHeight - (elementScrollHeight + elementHeight - 250) > 0;
        var isInTargetArea = (userScrolledHeight - elementScrollHeight) > 0;

        if (!isOverscrolled && isInTargetArea) {// && Math.abs(margin) < elementHeight) {
            this.isSet = false;
            this._element.nativeElement.classList.add('fixed');
            // todo replace with original top from subscribe
            this._element.nativeElement.style["marginTop"] = "80px";
        }

        else {
            this._element.nativeElement.classList.remove('fixed');

            if (margin < 0 && !this.isSet) {
                this.isSet = true;
                this._element.nativeElement.style["marginTop"] = Math.abs(margin) + "px";
            }
        }
    }
}