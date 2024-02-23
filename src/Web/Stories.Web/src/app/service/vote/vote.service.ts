import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VoteRequest } from '../../requestModel/voteRequest';
import { VoteViewModel } from '../../viewModel/voteViewModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VoteService {

  constructor(private httpClient: HttpClient) { }
  private url = 'http://localhost:5021/api/Votes';

  Post(voteRequest= new VoteRequest) {
    const headers = {'Content-Type': 'application/json'} ;
    const voteJson = JSON.stringify(voteRequest);
    return  this.httpClient.post(this.url , voteJson, {'headers': headers});
  } 
  Get(): Observable<VoteViewModel[]>{
    return  this.httpClient.get<VoteViewModel[]>(this.url)
   }
}
