import { Component, OnInit } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { StoryService } from '../../service/story/story.service';
import { StoryViewModel } from '../../viewModel/storyViewModel';
import { CommonModule } from '@angular/common';
import {MatIconModule} from '@angular/material/icon'
import { UserService } from '../../service/user/user.service';
import { UserViewModel } from '../../viewModel/userViewModel';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { VoteService } from '../../service/vote/vote.service';
import { VoteRequest } from '../../requestModel/voteRequest';
import { VoteViewModel } from '../../viewModel/voteViewModel';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatCardModule,
            MatButtonModule, 
            CommonModule, 
            MatIconModule, 
            MatLabel,
            MatFormFieldModule, 
            MatInputModule,
            ReactiveFormsModule, 
            FormsModule,
            MatOption,
            MatSelectModule, 
            MatFormFieldModule, 
            MatInputModule,
            ReactiveFormsModule, 
            FormsModule
          ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  formVoted : FormGroup ;
  
  constructor( private storyService: StoryService, private userService: UserService, 
               private formBuilder: FormBuilder, private voteService: VoteService ) {
                this.formVoted = new FormGroup({});
                this.formVoted = this.formBuilder.group({
                  userId: [0],
                });
  }
  
  stories:StoryViewModel[];
  users: UserViewModel[];
  votes: VoteViewModel[] = [] || undefined;
  votesId: VoteViewModel[] ;
  voteTrue: number;
  voteFalse: number;

  ngOnInit() {
    this.getUsers();
    this.getStories();
    this.getVotes();
   }

  ngAfterViewInit(): void {
    this.stories?.forEach(story => {
      this.countVotes(story);
    });
    this.orderStories();
  }
  
  getStories(){
    this.storyService.Get().subscribe((response: StoryViewModel[])  => {
      if(response)
      {
        this.stories = response;
      }
    } ,error => { console.error('Error get stories:', error);}); 
  }

  getUsers(){
    this.userService.Get().subscribe((response: UserViewModel[]) => {
      if(response ) 
      {
        this.users = response;
        this.formVoted = this.formBuilder.group({
          userId: [this.users[0].id], 
        });
      }
    }, error => {console.error('Error get users:', error)});
  } 

  postVoted(voteRequest : VoteRequest){
    this.voteService.Post(voteRequest).subscribe(response => {
      console.log('Response:', response);
    }, error => {console.error('Error:', error)});
  }

  getVotes(){
     this.voteService.Get().subscribe((response: VoteViewModel[]) => {
      if(response) 
      {
        this.votes = response;
      }
    }, error => {console.error('Error get votes:', error)});
  } 
  
  async onSubmit(voted: any, story: any){
    if(this.formVoted.valid){
      var vote = new VoteRequest
      vote.userId = this.formVoted.value.userId;
      vote.storyId = story;
      vote.voted = voted;
     
      this.postVoted(vote);
    }
    this.reload();
  }
  
  countVotes(story: StoryViewModel) {
    var votesId = this.votes.filter(s => s.storyId == story.id);
    this.voteTrue = votesId.filter(s => s.voted == true).length;
    this.voteFalse = votesId.filter(s => s.voted == false).length;

    if(this.voteTrue == this.voteFalse)
    {
      story.voteTrue = undefined;
      story.voteFalse = undefined;
      story.totalVoted = 0;
      //todas as vezes que ele se igualar teria que resetar os votos dessa história
    }else if(this.voteTrue>this.voteFalse){
        story.voteTrue = this.voteTrue - this.voteFalse;
        this.voteFalse = this.voteFalse - this.voteTrue;
        story.totalVoted = story.voteTrue;

        if (this.voteFalse > 0)
          story.voteFalse = this.voteFalse;
        else
          story.voteFalse = undefined;

    }else {
          story.voteFalse = this.voteFalse - this.voteTrue;
          this.voteTrue = this.voteTrue - this.voteFalse;
          if (this.voteTrue > 0)
          {
            story.voteTrue = this.voteTrue;
            story.totalVoted = this.voteTrue;
          }  
          else
          {
            story.voteTrue = undefined;
            story.totalVoted = 0;
          }  
        }
  }

  orderStories() {
    this.stories?.sort((a, b) => b.totalVoted - a.totalVoted); 
  }

  reload(){
    window.location.reload();
  }
}
