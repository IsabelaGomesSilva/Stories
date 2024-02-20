import { Component } from '@angular/core';
import { MatDialog, MatDialogRef, MatDialogActions,MatDialogClose,MatDialogTitle,
  MatDialogContent,} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';  
@Component({
  selector: 'app-add-story',
  standalone: true,
  imports: [MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent],
  templateUrl: './add-story.component.html',
  styleUrl: './add-story.component.css'
})
export class AddStoryComponent {
  constructor() {}
}
