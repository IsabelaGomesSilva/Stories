import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserViewModel } from '../../viewModel/userViewModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = 'http://localhost:5021/api/Users';
  
  constructor(private httpCliente: HttpClient) { }
  
  Get(): Observable<UserViewModel[]> {
    return  this.httpCliente.get<UserViewModel[]>(this.url);
  }
}
