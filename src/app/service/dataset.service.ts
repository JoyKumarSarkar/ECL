import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DatasetEndpoints } from '../type/endpoints';
import { MenuDetails, Return } from '../models/home.model';

const BaseUrl = environment.BaseApiUrl;

@Injectable({
  providedIn: 'root'
})

export class DatasetService {

  constructor(private router: Router, private http: HttpClient) { }

  GetDatasetList() {
    return this.http.get<any>(BaseUrl + DatasetEndpoints.GetDatasetList);
  }
}
