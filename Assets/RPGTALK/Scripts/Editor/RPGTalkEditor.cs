﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using RPGTALK.Texts;
using RPGTALK.Helper;


[CustomEditor(typeof(RPGTalk))]
public class RPGTalkEditor : Editor
{

    bool hideCharacters;

	override public void OnInspectorGUI()
	{
		serializedObject.Update ();

		//Instance of our RPGTalk class
		RPGTalk rpgTalk = (RPGTalk)target;

		EditorGUI.BeginChangeCheck();

		EditorGUILayout.LabelField("Put below the Text file to be parsed and become the talks!");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("txtToParse"),GUIContent.none);
		if (serializedObject.FindProperty ("txtToParse").objectReferenceValue == null) {
			EditorGUILayout.HelpBox ("RPGTalk needs a TXT file to retrieve the lines from", MessageType.Error, true);
		}

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("What line of the text should the Talk start? And in what line shoult it end?");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lineToStart"), GUIContent.none);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lineToBreak"), GUIContent.none);
        EditorGUILayout.EndHorizontal();
        string lineToStart = serializedObject.FindProperty("lineToStart").stringValue;
        int actualLinaToStart = -2;
        if (int.TryParse(lineToStart, out actualLinaToStart) && actualLinaToStart < 1)
        {
            EditorGUILayout.HelpBox("The line that the Text should start must be 1 or greater!", MessageType.Error, true);
        }
        string lineToBreak = serializedObject.FindProperty("lineToBreak").stringValue;
        int actualLinaToBreak = -2;
        if (int.TryParse(lineToBreak, out actualLinaToBreak) && actualLinaToBreak != -1 &&
            actualLinaToBreak < actualLinaToStart)
        {
            EditorGUILayout.HelpBox("The line of the Text to stop the Talk comes before the line of the Text to start the Talk? " +
                "That makes no sense! If you want to read the Text file until the end, leave the line to break as '-1'", MessageType.Error, true);
        }
        EditorGUILayout.HelpBox("The line to start or to end might be set as strings! For instance, you can set lineToStart as 'MyString' and in your text, RPGTalk will start reading the line just after the tag [title=MyString].", MessageType.Info, true);


        EditorGUILayout.PropertyField (serializedObject.FindProperty("showWithDialog"),true);
		if(serializedObject.FindProperty("showWithDialog").arraySize == 0){
			EditorGUILayout.HelpBox("Not a single element to be shown with the Talk? Not even the Canvas?" +
				"Are you sure that is the correct bahaviour?", MessageType.Warning, true);
		}

		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 

		EditorGUILayout.LabelField("Regular Options:",EditorStyles.boldLabel);
		rpgTalk.startOnAwake = GUILayout.Toggle(rpgTalk.startOnAwake, "Start On Awake?");
		rpgTalk.dialoger = GUILayout.Toggle(rpgTalk.dialoger, "Should try to read the name of the talker?");
		rpgTalk.shouldUsePhotos = GUILayout.Toggle(rpgTalk.shouldUsePhotos, "Should there be the photo of the talker?");
		rpgTalk.shouldStayOnScreen = GUILayout.Toggle(rpgTalk.shouldStayOnScreen, "Should the canvas stay on screen after the talk ended?");
		
		
		rpgTalk.enableQuickSkip = GUILayout.Toggle(rpgTalk.enableQuickSkip, "Enable QuickStep (the player can jump the animation)?");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("textSpeed"));

		rpgTalk.passWithMouse = GUILayout.Toggle(rpgTalk.passWithMouse, "Pass the Talk with Mouse Click?");
		EditorGUILayout.LabelField("The Talk can also be passed with some button set on Project Settings > Input:");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("passWithInputButton"),GUIContent.none);
        EditorGUILayout.LabelField("The Talk can also be passed with some key:");
        rpgTalk.passWithKey = (KeyCode)EditorGUILayout.EnumPopup(rpgTalk.passWithKey);
        rpgTalk.autoPass = GUILayout.Toggle(rpgTalk.autoPass, "Automatically Pass the Talk?");
        if (rpgTalk.autoPass)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("secondsAutoPass"));
        }
        if (!rpgTalk.passWithMouse && serializedObject.FindProperty("passWithInputButton").stringValue == "" && !rpgTalk.autoPass){
			EditorGUILayout.HelpBox("There is no condition to pass the Talk. Is it really the expected behaviour?", MessageType.Warning, true);
		}

		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("RPGTalk can try to make a Word Wrap for long texts in the same line.");
		rpgTalk.wordWrap = GUILayout.Toggle(rpgTalk.wordWrap, "Word Wrap?");
		if (rpgTalk.wordWrap) {
			EditorGUILayout.LabelField ("Set manually the maximum chars in Width/Height that fit in the screen:");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("maxCharInWidth"));
			EditorGUILayout.PropertyField (serializedObject.FindProperty ("maxCharInHeight"));
			EditorGUI.indentLevel--;

		}
        EditorGUILayout.LabelField("If lines to start and break were changed during the talk:");
        rpgTalk.goBackToOriginalStartAndBreak = GUILayout.Toggle(rpgTalk.goBackToOriginalStartAndBreak, "They should be changed back when it finish");

        EditorGUILayout.EndVertical ();


        if (rpgTalk.dialoger)
        {
            //create a nice box with round edges.
            EditorGUILayout.BeginVertical((GUIStyle)"HelpBox");

            EditorGUILayout.LabelField("Characters Settings:", EditorStyles.boldLabel);

            rpgTalk.shouldFollow = GUILayout.Toggle(rpgTalk.shouldFollow, "Should the canvas follow someone?");
            if (rpgTalk.shouldFollow && !rpgTalk.GetComponent<RPGTALK.Snippets.RPGTalkFollowCharacter>())
            {
                EditorGUILayout.HelpBox("There should be a RPGtalkFollowCharacter Snippet on this object to make the follow work the way it should", MessageType.Error, true);
                /*EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("The object that follows should be Billboard? Based on which camera?");
                EditorGUILayout.BeginHorizontal();
                rpgTalk.billboard = GUILayout.Toggle(rpgTalk.billboard, "Billboard?");
                if (rpgTalk.billboard)
                {
                    rpgTalk.mainCamera = GUILayout.Toggle(rpgTalk.mainCamera, "Based on Main Camera?");
                }
                EditorGUILayout.EndHorizontal();
                if (rpgTalk.billboard && !rpgTalk.mainCamera)
                {
                    EditorGUILayout.LabelField("What camera should the Billboard be based on?");
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("otherCamera"), GUIContent.none);
                }
                EditorGUI.indentLevel--;*/
            }


            EditorGUILayout.LabelField("Who are the characters that may appear on that conversation?");
            EditorGUI.indentLevel++;
            if (!hideCharacters)
            {
                if (rpgTalk.characters != null)
                {
                    for (int i = 0; i < rpgTalk.characters.Length; i++)
                    {
                        EditorGUILayout.LabelField("Who is this character?");
                        EditorGUILayout.BeginHorizontal();
                        rpgTalk.characters[i].character = (RPGTalkCharacter)EditorGUILayout.ObjectField(rpgTalk.characters[i].character, typeof(RPGTalkCharacter), false);
                        if (GUILayout.Button(" - "))
                        {
                            serializedObject.FindProperty("characters").DeleteArrayElementAtIndex(i);
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUI.indentLevel += 2;
                        EditorGUILayout.LabelField("Should an different Animator be set for it?");
                        rpgTalk.characters[i].animatorOverwrite = (Animator)EditorGUILayout.ObjectField(rpgTalk.characters[i].animatorOverwrite, typeof(Animator), true);
                        if (rpgTalk.shouldFollow)
                        {
                            EditorGUILayout.LabelField("What Transform should the canvas follow? Should there be an offset?");
                            EditorGUILayout.BeginHorizontal();
                            rpgTalk.characters[i].follow = (Transform)EditorGUILayout.ObjectField(rpgTalk.characters[i].follow, typeof(Transform), true);
                            rpgTalk.characters[i].followOffset = EditorGUILayout.Vector3Field(GUIContent.none, rpgTalk.characters[i].followOffset);
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUI.indentLevel -= 2;
                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    }
                }
            }
            EditorGUILayout.BeginHorizontal();
            if (!hideCharacters)
            {
                if (GUILayout.Button(" + "))
                {
                    serializedObject.FindProperty("characters").InsertArrayElementAtIndex(serializedObject.FindProperty("characters").arraySize);
                }
            }
            if (GUILayout.Button(hideCharacters ? "Show " + rpgTalk.characters.Length + " characters" : "Hide Characters"))
            {
                hideCharacters = !hideCharacters;
            }
            EditorGUILayout.EndHorizontal();

            if (rpgTalk.shouldFollow && rpgTalk.characters.Length == 0)
            {
                EditorGUILayout.HelpBox("You need to set a Character and its Transform so that the Canvas can follow someone", MessageType.Error, true);
            }


            EditorGUI.indentLevel--;



            EditorGUILayout.EndVertical();


        }


        //create a nice box with round edges.
        EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Interface:",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Put below the UI for the text itself:");
        EditorGUILayout.PropertyField (serializedObject.FindProperty("textUIObj"));

        if (serializedObject.FindProperty("textUIObj").objectReferenceValue == null){
			EditorGUILayout.HelpBox("There should be a Text, inside of a Canvas, to show the Talk.", MessageType.Error, true);
		}else if (!TMP_Translator.IsValidType(serializedObject.FindProperty("textUIObj").objectReferenceValue as GameObject))
        {
            EditorGUILayout.HelpBox("The object must be a Text or a Text Mesh Pro UGUI Type", MessageType.Error, true);
        }

        if (rpgTalk.dialoger) {
			EditorGUILayout.LabelField("Put below the UI for the name of the talker:");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogerObj"));
            if (serializedObject.FindProperty("dialogerObj").objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("There should be a Text, inside of a Canvas, to show the name of the talker.", MessageType.Warning, true);
            }
            else if (!TMP_Translator.IsValidType(serializedObject.FindProperty("dialogerObj").objectReferenceValue as GameObject))
            {
                EditorGUILayout.HelpBox("The object must be a Text or a Text Mesh Pro UGUI Type", MessageType.Error, true);
            }

		}
		if (rpgTalk.shouldUsePhotos) {
			EditorGUILayout.LabelField("Put below the UI for the photo of the talker:");
			EditorGUILayout.PropertyField (serializedObject.FindProperty("UIPhoto"));
			if(serializedObject.FindProperty("UIPhoto").objectReferenceValue == null){
				EditorGUILayout.HelpBox("There should be a Image, inside of a Canvas, to show the talker's photo.", MessageType.Warning, true);
			}
		}
		EditorGUILayout.EndVertical ();


		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Callback & Variables:",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Any script should be called when the Talk is done?");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("callback"),GUIContent.none);

		
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField("Variables can be set to change some word in the text");
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField (serializedObject.FindProperty("variables"),true);
		EditorGUI.indentLevel--;

		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Photos and Sprites:",EditorStyles.boldLabel);


        if (rpgTalk.textUIObj != null && TMP_Translator.IsText(rpgTalk.textUIObj))
        {
            EditorGUILayout.LabelField("Add sprites that can be used inside the text:");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprites"), true);
            EditorGUI.indentLevel--;
            if (rpgTalk.sprites != null && rpgTalk.sprites.Count > 0)
            {
                EditorGUILayout.HelpBox("To use sprites inside the text, write the tag [sprite=X] and RPGTalk will replace it with the corresponding sprite above", MessageType.Info, true);
            }
        }
        else if(rpgTalk.textUIObj != null)
        {
            EditorGUILayout.LabelField("Write below the name of the Sprite Atlas Asset you want Text Mesh Pro to use:");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tmpSpriteAtlas"), true);
            EditorGUILayout.HelpBox("To use sprites inside the text, write the tag [sprite=X] in your TXT and RPGTalk will replace it with the  Text Mesh Pro tag", MessageType.Info, true);
        }



        EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Animation:",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("A Animator can be manipulated when talking:");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("animatorWhenTalking"),GUIContent.none);
		if(serializedObject.FindProperty("animatorWhenTalking").objectReferenceValue != null){
			EditorGUILayout.LabelField("A Boolean to be set true when the character is talking:");
			EditorGUILayout.PropertyField (serializedObject.FindProperty("animatorBooleanName"),GUIContent.none);
			EditorGUILayout.LabelField("A int can be set with the number of the talker, based on the list of photos:");
			EditorGUILayout.PropertyField (serializedObject.FindProperty("animatorIntName"),GUIContent.none);
		}
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Audio:",EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Audio Source:");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rpgAudioSorce"), GUIContent.none);
        EditorGUILayout.LabelField("Audio Source for Letters:");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("letterSource"), GUIContent.none);
        EditorGUILayout.LabelField("The audio to be played by each letter:");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("textAudio"),GUIContent.none);
		EditorGUILayout.LabelField("The audio to be played when the player passes the Talk:");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("passAudio"),GUIContent.none);
		EditorGUILayout.EndVertical ();

		EditorGUILayout.BeginVertical( (GUIStyle) "HelpBox"); 
		EditorGUILayout.LabelField("Choices:",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Put below a Button prefab that will be instantiated when the player has choices");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("choicePrefab"),GUIContent.none);
		EditorGUILayout.LabelField("Put below a object that the choice buttons will be instantiated to:");
		EditorGUILayout.PropertyField (serializedObject.FindProperty("choicesParent"),GUIContent.none);
		if(serializedObject.FindProperty("choicePrefab").objectReferenceValue != null && serializedObject.FindProperty("choicesParent").objectReferenceValue == null){
			EditorGUILayout.HelpBox("There should be a parent to instantiate the choices to. Usually, it as an object inside the dialog box with a 'Layout Group' element.", MessageType.Warning, true);
		}
		EditorGUILayout.EndVertical ();

		EditorGUILayout.Separator ();

		if(EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties();
		//EditorGUIUtility.LookLikeControls();

	}
}