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

import { IngredientDetailModel } from '../models/ingredient-detail-model';
import { IngredientListModel } from '../models/ingredient-list-model';

@Injectable({
  providedIn: 'root',
})
export class IngredientService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation ingredientGetAll
   */
  static readonly IngredientGetAllPath = '/api/Ingredient';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `ingredientGetAll()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientGetAll$Response(params?: {
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<Array<IngredientListModel>>> {

    const rb = new RequestBuilder(this.rootUrl, IngredientService.IngredientGetAllPath, 'get');
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
        return r as StrictHttpResponse<Array<IngredientListModel>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `ingredientGetAll$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientGetAll(params?: {
    version?: string;
    culture?: any;
  }): Observable<Array<IngredientListModel>> {

    return this.ingredientGetAll$Response(params).pipe(
      map((r: StrictHttpResponse<Array<IngredientListModel>>) => r.body as Array<IngredientListModel>)
    );
  }

  /**
   * Path part for operation ingredientUpdate
   */
  static readonly IngredientUpdatePath = '/api/Ingredient';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `ingredientUpdate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  ingredientUpdate$Response(params: {
    version?: string;
    culture?: any;
    body: IngredientDetailModel
  }): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, IngredientService.IngredientUpdatePath, 'put');
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
   * To access the full response (for headers, for example), `ingredientUpdate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  ingredientUpdate(params: {
    version?: string;
    culture?: any;
    body: IngredientDetailModel
  }): Observable<string> {

    return this.ingredientUpdate$Response(params).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * Path part for operation ingredientCreate
   */
  static readonly IngredientCreatePath = '/api/Ingredient';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `ingredientCreate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  ingredientCreate$Response(params: {
    version?: string;
    culture?: any;
    body: IngredientDetailModel
  }): Observable<StrictHttpResponse<string>> {

    const rb = new RequestBuilder(this.rootUrl, IngredientService.IngredientCreatePath, 'post');
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
   * To access the full response (for headers, for example), `ingredientCreate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  ingredientCreate(params: {
    version?: string;
    culture?: any;
    body: IngredientDetailModel
  }): Observable<string> {

    return this.ingredientCreate$Response(params).pipe(
      map((r: StrictHttpResponse<string>) => r.body as string)
    );
  }

  /**
   * Path part for operation ingredientGetById
   */
  static readonly IngredientGetByIdPath = '/api/Ingredient/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `ingredientGetById()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientGetById$Response(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<IngredientDetailModel>> {

    const rb = new RequestBuilder(this.rootUrl, IngredientService.IngredientGetByIdPath, 'get');
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
        return r as StrictHttpResponse<IngredientDetailModel>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `ingredientGetById$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientGetById(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<IngredientDetailModel> {

    return this.ingredientGetById$Response(params).pipe(
      map((r: StrictHttpResponse<IngredientDetailModel>) => r.body as IngredientDetailModel)
    );
  }

  /**
   * Path part for operation ingredientDelete
   */
  static readonly IngredientDeletePath = '/api/Ingredient/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `ingredientDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientDelete$Response(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<StrictHttpResponse<Blob>> {

    const rb = new RequestBuilder(this.rootUrl, IngredientService.IngredientDeletePath, 'delete');
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
   * To access the full response (for headers, for example), `ingredientDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  ingredientDelete(params: {
    id: string;
    version?: string;
    culture?: any;
  }): Observable<Blob> {

    return this.ingredientDelete$Response(params).pipe(
      map((r: StrictHttpResponse<Blob>) => r.body as Blob)
    );
  }

}
