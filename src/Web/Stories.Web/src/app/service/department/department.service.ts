import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private url = 'http://localhost:5021/api/Departments';
  constructor(private httpClient: HttpClient) { }
  Get(){
   return this.httpClient.get(this.url)
  }
}
