import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs/Rx';
import { Badge } from '../domain/dto/badge';
import { HttpClient } from '../helpers/httpClient';
import { GetBadgesResponse } from '../domain/dto/getBadgesResponse';

@Injectable()
export class BadgeService {
  constructor(private http: HttpClient, private configService: ConfigService) {  }

  private badgeApiUri = `${this.configService.apiPrefix}/badges`;

  getBadges(): Observable<GetBadgesResponse> {
    return this.http.get(`${this.badgeApiUri}`)
      .map(this.mapBadges)
      .catch(this.handleError);
  }

  postOrderBadge(): Observable<string[]> {
    return this.http.post(`${this.badgeApiUri}/order`, null)
      .map(this.mapBadgeAlerts)
      .catch(this.handleError);
  }

  postPrepayBadge(username: string): Observable<string[]> {
    return this.http.post(`${this.badgeApiUri}/prepay`, { "username": username })
      .map(this.mapBadgeAlerts)
      .catch(this.handleError);
  }

  private mapBadgeAlerts(res: Response): string[] {
    let body = res.json();
    return body;
  }

  private mapBadges(res: Response): GetBadgesResponse {
    let body = res.json();
    var response = new GetBadgesResponse().deserialize(body);

    return response;
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