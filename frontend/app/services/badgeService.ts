import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './configService';
import { Observable } from 'rxjs/Rx';
import { Badge } from '../domain/dto/badge';
import { HttpClient } from '../helpers/httpClient';

@Injectable()
export class BadgeService {
  constructor(private http: HttpClient, private configService: ConfigService) {  }

  private badgeApiUri = `${this.configService.apiPrefix}/badges`;

  getBadges(): Observable<Badge[]> {
    return this.http.get(`${this.badgeApiUri}`)
      .map(this.mapBadges)
      .catch(this.handleError);
  }

  private mapBadges(res: Response): Badge[] {
    let body = res.json();

    // todo, deserialize in domain object.
    var badges: Badge[] = body;
    return badges;
  }

  private handleError(error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}