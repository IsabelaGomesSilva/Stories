import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { storyViewModel } from '../../viewModel/storyViewModel';
@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private url = 'http://localhost:5021/api/Stories';
  constructor(private httpClient: HttpClient) { }
  Get(): Observable<storyViewModel[]>{
   return  this.httpClient.get<storyViewModel[]>(this.url)
  }

  
}
