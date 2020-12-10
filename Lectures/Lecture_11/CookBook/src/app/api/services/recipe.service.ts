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

import { RecipeDetailModel } from '../models/recipe-detail-model';
import { RecipeListModel } from '../models/recipe-list-model';

@Injectable({
  providedIn: 'root',
})
export class RecipeService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation recipeGetAll
   */
  static readonly RecipeGetAllPath = '/api/Recipe';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `recipeGetAll()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeGetAll$Response(params?: {
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<Array<RecipeListModel>>> {

    const rb = new RequestBuilder(this.rootUrl, RecipeService.RecipeGetAllPath, 'get');
    if (params) {
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<RecipeListModel>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `recipeGetAll$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeGetAll(params?: {
    version?: string;
    culture?: any;
  }): Observable<Array<RecipeListModel>> {

    return this.recipeGetAll$Response(params).pipe(
      map((r: StrictHttpResponse<Array<RecipeListModel>>) => r.body as Array<RecipeListModel>)
    );
  }

  /**
   * Path part for operation recipeUpdate
   */
  static readonly RecipeUpdatePath = '/api/Recipe';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `recipeUpdate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  recipeUpdate$Response(params: {
    version?: string;
    culture?: any;
    body: RecipeDetailModel
  }): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, RecipeService.RecipeUpdatePath, 'put');
    if (params) {
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<string>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `recipeUpdate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  recipeUpdate(params: {
    version?: string;
    culture?: any;
    body: RecipeDetailModel
  }): Observable<string> {

    return this.recipeUpdate$Response(params).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * Path part for operation recipeCreate
   */
  static readonly RecipeCreatePath = '/api/Recipe';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `recipeCreate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  recipeCreate$Response(params: {
    version?: string;
    culture?: any;
    body: RecipeDetailModel
  }): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, RecipeService.RecipeCreatePath, 'post');
    if (params) {
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<string>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `recipeCreate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  recipeCreate(params: {
    version?: string;
    culture?: any;
    body: RecipeDetailModel
  }): Observable<string> {

    return this.recipeCreate$Response(params).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * Path part for operation recipeGetById
   */
  static readonly RecipeGetByIdPath = '/api/Recipe/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `recipeGetById()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeGetById$Response(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<RecipeDetailModel>> {

    const rb = new RequestBuilder(this.rootUrl, RecipeService.RecipeGetByIdPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<RecipeDetailModel>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `recipeGetById$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeGetById(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<RecipeDetailModel> {

    return this.recipeGetById$Response(params).pipe(
      map((r: StrictHttpResponse<RecipeDetailModel>) => r.body as RecipeDetailModel)
    );
  }

  /**
   * Path part for operation recipeDelete
   */
  static readonly RecipeDeletePath = '/api/Recipe/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `recipeDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeDelete$Response(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<Blob>> {

    const rb = new RequestBuilder(this.rootUrl, RecipeService.RecipeDeletePath, 'delete');
    if (params) {
      rb.path('id', params.id, {});
      rb.query('version', params.version, {});
      rb.query('culture', params.culture, {});
    }

    return this.http.request(rb.build({
      responseType: 'blob',
      accept: 'application/octet-stream'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Blob>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `recipeDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  recipeDelete(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<Blob> {

    return this.recipeDelete$Response(params).pipe(
      map((r: StrictHttpResponse<Blob>) => r.body as Blob)
    );
  }

}
