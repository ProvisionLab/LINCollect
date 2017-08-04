//
//  RegistrationVC.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "RegistrationVC.h"

@interface RegistrationVC ()
{
    CGRect  mainViewFrame;
    
    __weak IBOutlet UIView *mainView;
    
    __weak IBOutlet UITextField *userNameTF;
    __weak IBOutlet UITextField *passwordTF;
    __weak IBOutlet UITextField *repearPasswordTF;
    __weak IBOutlet UITextField *emailTF;
    
    __weak IBOutlet UIButton *registrButton;
}
- (IBAction)registrButton:(UIButton *)sender;

@end

@implementation RegistrationVC

#pragma mark LifeCicle
- (void)viewDidLoad {
    [super viewDidLoad];
    [self setView];
    [self setNavigation];
    [self setNotification];
}

-(void) viewDidAppear:(BOOL)animated {
    mainViewFrame = mainView.frame;
}

-(void) viewDidDisappear:(BOOL)animated {
    [self.view endEditing:YES];
}

-(void) viewWillDisappear:(BOOL)animated {
    [self.navigationController setNavigationBarHidden:YES animated:YES];
}

-(void) dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

#pragma mark Methods
-(void) setView {
    [userNameTF setPlaceholderColor];
    [passwordTF setPlaceholderColor];
    [repearPasswordTF setPlaceholderColor];
    [emailTF setPlaceholderColor];
}

-(void) setNavigation {
    self.navigationItem.title = @"Registration";
    [self.navigationController setNavigationBarHidden:NO];
}

-(void) setNotification {
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(keyboardWillShow:) name:UIKeyboardWillShowNotification object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(keyboardWillHide:) name:UIKeyboardWillHideNotification object:nil];
}

#pragma mark Actions
- (IBAction)registrButton:(UIButton *)sender {
    
}

#pragma mark UITextFieldDelegate
-(BOOL)textFieldShouldReturn:(UITextField *)textField
{
    if ([textField isEqual:userNameTF]) {
        [passwordTF becomeFirstResponder];
    }else if ([textField isEqual:passwordTF]) {
        [repearPasswordTF becomeFirstResponder];
    }else if ([textField isEqual:repearPasswordTF]) {
        [emailTF becomeFirstResponder];
    } else if ([textField isEqual:emailTF]) {
        [emailTF resignFirstResponder];
    }
    return YES;
}

#pragma mark KeyboardNotification
- (void)keyboardWillShow:(NSNotification*)notification
{
    if (mainView.frame.origin.y == mainViewFrame.origin.y) {
        NSDictionary* keyboardInfo = [notification userInfo];
        NSValue* keyboardFrameBegin = [keyboardInfo valueForKey:UIKeyboardFrameBeginUserInfoKey];
        CGRect keyboardRect = [keyboardFrameBegin CGRectValue];
        CGRect bVRect = mainView.frame;
        mainView.frame = CGRectMake(bVRect.origin.x, bVRect.origin.y - keyboardRect.size.height + (bVRect.origin.y - 1), bVRect.size.width, bVRect.size.height);
    }
}

- (void)keyboardWillHide:(NSNotification*)notification
{
    NSDictionary* keyboardInfo = [notification userInfo];
    NSValue* keyboardFrameBegin = [keyboardInfo valueForKey:UIKeyboardFrameBeginUserInfoKey];
    CGRect keyboardRect = [keyboardFrameBegin CGRectValue];
    CGRect bVRect = mainView.frame;
    mainView.frame = CGRectMake(bVRect.origin.x, bVRect.origin.y + keyboardRect.size.height - (mainViewFrame.origin.y - 1), bVRect.size.width, bVRect.size.height);
}
@end
