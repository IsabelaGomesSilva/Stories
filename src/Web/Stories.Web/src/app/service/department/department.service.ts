import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DepartmentViewModel } from '../../viewModel/departmentViewModel';
@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private url = 'http://localhost:5021/api/Departments';
  constructor(private httpClient: HttpClient) { }
  
  Get(): Observable<DepartmentViewModel[]>{
   return this.httpClient.get<DepartmentViewModel[]>(this.url)
  }
}
