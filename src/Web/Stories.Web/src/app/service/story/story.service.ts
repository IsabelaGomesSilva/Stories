import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StoryRequest } from '../../requestModel/storyRequest';
import { StoryViewModel } from '../../viewModel/storyViewModel';
@Injectable({
  providedIn: 'root'
})
export class StoryService {
  private url = 'http://localhost:5021/api/Stories';
  constructor(private httpClient: HttpClient) { }

  Get(): Observable<StoryViewModel[]>{
   return  this.httpClient.get<StoryViewModel[]>(this.url)
  }
  GetStory(id: any): Observable<StoryViewModel>{
    return  this.httpClient.get<StoryViewModel>(this.url+"/"+id)
   }

  Post(storyRequest = new StoryRequest) {
    const headers = {'Content-Type': 'application/json'} ;
    const storyJson = JSON.stringify(storyRequest);
    return  this.httpClient.post(this.url, storyJson, {'headers': headers});
  } 
  Put(id: any, storyRequest = new StoryRequest){
    const headers = {'Content-Type': 'application/json'} ;
    const storyJson = JSON.stringify(storyRequest);
    return  this.httpClient.put(this.url+"/"+id, storyJson, {'headers': headers});
  }
  Delete(id: any)
  {
    return  this.httpClient.delete(this.url+"/"+id);
  }
  
}
