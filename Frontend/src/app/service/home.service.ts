import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HomeEndpoints } from '../type/endpoints';
import { MenuDetails, Return } from '../models/home.model';

const BaseUrl = environment.BaseApiUrl;

@Injectable({
  providedIn: 'root'
})

export class HomeService {

  constructor(private router: Router, private http: HttpClient) { }

  GetMenu() {
    return this.http.get<Return<MenuDetails[]>>(BaseUrl + HomeEndpoints.GetMenu);
  }
}
