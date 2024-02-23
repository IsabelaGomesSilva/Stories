import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { StoryService } from '../../service/story/story.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-dialog-delete',
  standalone: true,
  imports: [CommonModule, 
            MatButtonModule, 
            MatDialogActions, 
            MatDialogClose, 
             MatDialogTitle, 
             MatDialogContent
            ],
  templateUrl: './dialog-delete.component.html',
  styleUrl: './dialog-delete.component.css'
})
export class DialogDeleteComponent {
  /**
   *
   */
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private storyService: StoryService) {}

  delete(){
    console.log(this.data.id);
    if(this.data.id>0){
      this.storyService.Delete(this.data.id).subscribe( response => {
        if(response)
        {
          console.log('História deletada');
        }
      } ,error => { console.error('Erro ao obter histórias:', error);}); 
    }
  }

}
