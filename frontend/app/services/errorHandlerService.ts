import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { ErrorDescriptor } from '../domain/dto/errorDescriptor'

@Injectable()
export class ErrorHandlerService {
    public handleError(error: any): Observable<any> {
        var errorDescriptor = new ErrorDescriptor().deserialize(error);
        console.error(errorDescriptor.toString());
        return Observable.throw(errorDescriptor);
    }
}


