import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { StoryService } from '../../service/story/story.service';
import { StoryViewModel } from '../../viewModel/storyViewModel';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import {MatIcon, MatIconModule} from '@angular/material/icon'
import { Router } from '@angular/router';
import { DialogDeleteComponent } from '../dialog-delete/dialog-delete.component';



@Component({
  selector: 'app-story',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatButtonModule, MatIcon],
  templateUrl: './story.component.html',
  styleUrl: './story.component.css'
})
export class StoryComponent implements OnInit, AfterViewInit {
  constructor (private storyService: StoryService, 
               private dialog: MatDialog,
               private router: Router ) {}
  
  displayedColumns: string[] = ['Title', 'Description', 'Department', 'Action'];
 
  dataSource: MatTableDataSource<StoryViewModel> = new MatTableDataSource<StoryViewModel>();
  story!: StoryViewModel;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(){
    this.get();
   }

   get(){
    this.storyService.Get().subscribe((response: StoryViewModel[])  => {
      if(response)
      {
        this.dataSource.data = response;
        this.ngAfterViewInit();
      }
    } ,error => { console.error('Erro ao obter histórias:', error);}); 
   }
  updateStory(id: any){
     this.openDialog(id,'Editar ');
     
  }
  addStory(){
    this.openDialog(0,'Adicionar ');
   }

  deleteStory(id: any){
    this.storyService.Delete(id).subscribe( response => {
      if(response)
      {
        console.log('História deletada');
      }
    } ,error => { console.error('Erro ao obter histórias:', error);}); 
    this.reload();
  }

   ngAfterViewInit(): void { this.dataSource.paginator = this.paginator;}

   openDialog(id:any, title: any): void {
      let dialogResult = this.dialog.open(DialogComponent, {
      enterAnimationDuration:'1000ms',
      exitAnimationDuration:'1000ms',
      data:{
        title:title,
        id:id
      }
    })
    dialogResult.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`); 
      this.reload();
    });
  }

  openDeleteDialog(id:any): void {
    console.log(id);
    let dialogResult = this.dialog.open(DialogDeleteComponent, {
    enterAnimationDuration:'0',
    exitAnimationDuration:'0',
    data:{
      id:id
   }
  })
  dialogResult.afterClosed().subscribe(result => {
    console.log(`Dialog result: ${result}`); 
     this.reload();
  });
}
  

    reload(){
      window.location.reload();
    }
  
}
