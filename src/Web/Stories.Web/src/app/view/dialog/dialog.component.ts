import { Component, Inject, OnInit } from '@angular/core';
import {  MatDialogActions,MatDialogClose,MatDialogTitle,
         MatDialogContent,
         MAT_DIALOG_DATA,} from '@angular/material/dialog';
import {MatButtonModule} from '@angular/material/button';  
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup} from '@angular/forms';
import { DepartmentService } from '../../service/department/department.service';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CommonModule } from '@angular/common';
import { MatLabel } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { StoryViewModel } from '../../viewModel/storyViewModel';
import { DepartmentViewModel } from '../../viewModel/departmentViewModel';
import { StoryService } from '../../service/story/story.service';
import {MatInputModule} from '@angular/material/input';


@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [CommonModule, 
            MatButtonModule, 
            MatDialogActions, 
            MatDialogClose, 
            MatDialogTitle, 
            MatDialogContent,
            MatAutocompleteModule,
            MatLabel, 
            MatSelectModule, 
            MatFormFieldModule, 
            MatInputModule,
            ReactiveFormsModule, 
            FormsModule,
          ],

  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.css'
})
export class DialogComponent implements OnInit {
  
   departments!: DepartmentViewModel[];
   story!: StoryViewModel;
   updateStory!:any;

   myForm : FormGroup = new FormGroup({});

   constructor(@Inject(MAT_DIALOG_DATA) public data: any, private departmentService: DepartmentService,
               private formBuilder: FormBuilder,
               private storyService: StoryService) 
               {
                this.myForm = this.formBuilder.group({
                  title:[''], 
                  description: [''], 
                  departmentId:[0]
                })
              }

   ngOnInit(){
    this.getDepartment();
    this.updateStory = this.data;
    if (this.updateStory.id > 0){
      this.setStory(this.updateStory.id);
    }
  }

  getDepartment(){
    this.departmentService.Get().subscribe((response: DepartmentViewModel[])  => {
      if(response)
      {
        this.departments = response;
      }
    } ,error => { console.error('Erro departaments:', error);}); 
  }

  async onSubmit(){
    if(this.data.id==0){
      if( this.myForm.valid){
       this.storyService.Post(this.myForm.value).subscribe(response => {
        console.log('Resposta do servidor:', response);});
   
      }
    }else 
    {
      if( this.myForm.valid){
        this.storyService.Put(this.data.id, this.myForm.value).subscribe(response => {
         console.log('Resposta do servidor:', response);});
    
       }
    }
      
  }
  setStory(id: any){
    this.storyService.GetStory(id).subscribe((response: StoryViewModel)  => {
      if(response)
      {
        this.story = response;
        this.myForm.setValue({
          title:this.story.title, 
          description: this.story.description, 
          departmentId:this.story.departmentId
        });
      }
    } ,error => { console.error('Erro ao obter histórias:', error);}); 
   }
 
   
}

