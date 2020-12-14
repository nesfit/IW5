/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';


@Injectable({
  providedIn: 'root',
})
export class LoggingService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation loggingLogSomething
   */
  static readonly LoggingLogSomethingPath = '/api/Logging';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `loggingLogSomething()` instead.
   *
   * This method doesn't expect any request body.
   */
  loggingLogSomething$Response(params?: {
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, LoggingService.LoggingLogSomethingPath, 'get');
    if (params) {
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `loggingLogSomething$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  loggingLogSomething(params?: {
    version?: string;
    culture?: any;
  }): Observable<void> {

    return this.loggingLogSomething$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
