import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { departmentViewModel } from '../../viewModel/departmentViewModel';
@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private url = 'http://localhost:5021/api/Departments';
  constructor(private httpClient: HttpClient) { }
  
  Get(): Observable<departmentViewModel[]>{
   return this.httpClient.get<departmentViewModel[]>(this.url)
  }
}
